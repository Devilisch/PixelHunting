using UnityEngine;

using Models;
using ScriptableObjects;
using Views;

namespace Controllers
{
    public class RatController : EntityController
    {
        public EnemyView View { get; private set; }



        public void Init(EnemyView prefab, EntityScriptableObject entityScriptableObject, Transform spawnTransform)
        {
            Speed = entityScriptableObject.Speed;
        
            View = Object.Instantiate(prefab, spawnTransform);
            View.Init(entityScriptableObject);
            View.AddCollisionListener(OnHitCollision);
            entityView = View;
        
            OnSpawn();
        }

        public void OnSpawn()
        {
            View.SetState(EntityState.IDLE);
            View.transform.position = Utilities.GetRandomStagePositions();
        }
    }
}
