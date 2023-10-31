using System;
using System.Collections;
using UnityEngine;

using ScriptableObjects;

namespace Views
{
    [RequireComponent(typeof(SpriteRenderer),typeof(Animator),typeof(Collider2D))]
    public class EntityView : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private float hitStunTime;
        private Action<Collision2D> collisionAction;
        private Action<Collider2D> triggerAction;

        private static readonly int IdleAnimationHash = Animator.StringToHash("Idle");
        private static readonly int WalkAnimationHash = Animator.StringToHash("Walk");
        private static readonly int HitAnimationHash = Animator.StringToHash("Hit");
        private static readonly int DeathAnimationHash = Animator.StringToHash("Death");

        public EntityState State { get; private set; } = EntityState.NONE;
        public EntityType Type { get; private set; } = EntityType.NONE;

    

    
        public void Init(EntityScriptableObject entityScriptableObject)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            spriteRenderer.sprite = entityScriptableObject.Sprite;
            hitStunTime = entityScriptableObject.HitStunTime;
            Type = entityScriptableObject.Type;

            if (entityScriptableObject.AnimatorController != null)
            {
                animator.enabled = true;
                animator.runtimeAnimatorController = entityScriptableObject.AnimatorController;
            }
        }

        public void OnSpawn()
        {
            State = EntityState.IDLE;
            SetAnimation(IdleAnimationHash);
        }

        public void SetState(EntityState state, bool isActive = true)
        {
            if (State == state || State == EntityState.DEATH)
                return;

            State = state;
            
            switch (state)
            {
                default:
                case EntityState.IDLE:
                    SetAnimation(IdleAnimationHash);
                    break;
                case EntityState.WALK:
                    SetAnimation(WalkAnimationHash, isActive);
                    break;
                case EntityState.HIT:
                    SetAnimation(HitAnimationHash);
                    StartCoroutine(Stunning());
                    break;
                case EntityState.DEATH:
                    SetAnimation(DeathAnimationHash);
                    break;
            }
        }
    
        private IEnumerator Stunning()
        {
            yield return new WaitForSeconds(hitStunTime);

            SetState(EntityState.IDLE);
        }

        private void SetAnimation(int nameHash)
        {
            if (!animator.enabled)
                return;
            
            SetAnimation(WalkAnimationHash, false);
            animator.SetTrigger(nameHash);
        }

        private void SetAnimation(int nameHash, bool state)
        {
            if (!animator.enabled)
                return;
            
            animator.SetBool(nameHash, state);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        public void SetSpriteFlip(bool isFlipped = false) => spriteRenderer.flipX = isFlipped;

        private void OnCollisionEnter2D(Collision2D col) 
        {
            SetState(EntityState.HIT);
            collisionAction?.Invoke(col);
        }
    
        public void AddCollisionListener(Action<Collision2D> action) => collisionAction += action;
        public void RemoveCollisionListener(Action<Collision2D> action) => collisionAction -= action;
    
        private void OnTriggerEnter2D(Collider2D col) => triggerAction?.Invoke(col);
        public void AddTriggerListener(Action<Collider2D> action) => triggerAction += action;
        public void RemoveTriggerListener(Action<Collider2D> action) => triggerAction -= action;
    }
}
