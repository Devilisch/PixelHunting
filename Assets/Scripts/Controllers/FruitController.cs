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
