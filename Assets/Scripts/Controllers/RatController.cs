using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : EntityController
{
    public EnemyView View { get; private set; }
    public PlayerModel Model { get; private set; }



    public void Init(EnemyView prefab, EntityScriptableObject entityScriptableObject, Transform spawnTransform)
    {
        Speed = entityScriptableObject.Speed;
        
        View = Object.Instantiate(prefab, spawnTransform);
        View.Init(entityScriptableObject);
        View.AddCollisionListener(OnHitCollision);
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
}
