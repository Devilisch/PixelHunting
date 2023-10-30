using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : EntityController
{
    public FruitView View { get; private set; }
    public PlayerModel Model { get; private set; }



    public void Init(FruitView prefab, EntityScriptableObject entityScriptableObject, Transform spawnTransform)
    {
        View = Object.Instantiate(prefab, spawnTransform);
        View.Init(entityScriptableObject);
        View.AddTriggerListener(OnTriggerEnter2D);
        entityView = View;

        Model = new PlayerModel();
        
        OnSpawn();
    }

    public void OnSpawn()
    {
        Model.OnSpawn();
        
        View.SetState(EntityState.IDLE);
        View.transform.position = Utilities.GetRandomStagePositions();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var entity = col.gameObject.GetComponent<EntityView>();

        if (entity == null)
            return;
        
        switch (entity.Type)
        {
            default:
                break;
            case EntityType.PLAYER:
                GameManager.Instance.ScoreController.AddPoints(View.Type);
                GameManager.Instance.FruitManager.TakeFruit(this);
                break;
        }
    }
}
