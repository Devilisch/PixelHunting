using UnityEngine;

using Managers;
using Models;
using ScriptableObjects;
using Views;

namespace Controllers
{
    public class PlayerController : EntityController
    {
        public PlayerView View { get; private set; }
        public PlayerModel Model { get; private set; }



        public void Init(PlayerView prefab, EntityScriptableObject entityScriptableObject, Transform spawnTransform)
        {
            Speed = entityScriptableObject.Speed;
        
            View = Object.Instantiate(prefab, spawnTransform);
            View.Init(entityScriptableObject);
            View.AddCollisionListener(OnHitCollision);
            View.AddCollisionListener(OnCollisionEnter2D);
            entityView = View;

            Model = new PlayerModel();
        }

        public void OnSpawn()
        {
            Model.OnSpawn();

            View.OnSpawn();
            View.transform.position = Vector3.zero;
        }

        private void OnDamage(int value)
        {
            Model.OnDamage(value);

            if (Model.Health == 0)
            {
                View.SetState(EntityState.DEATH);
                GameManager.Instance.GameplayController.OnPlayerDeath();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var entity = col.gameObject.GetComponent<EntityView>();

            if (entity == null)
                return;
        
            switch (entity.Type)
            {
                default:
                    break;
                case EntityType.RAT:
                    OnDamage(1);
                    break;
            }
        }
    }
}
