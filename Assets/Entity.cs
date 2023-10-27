using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Animator),typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected new Rigidbody2D rigidbody;
    [SerializeField] protected float onHitPause = 0.25f;
    
    protected Coroutine _onHitCoroutine = null;
    
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");

    

    protected IEnumerator OnHit()
    {
        OnAnimate(AnimationType.HIT);

        yield return new WaitForSeconds(onHitPause);

        _onHitCoroutine = null;
    }
    
    public void OnMove(Vector3 moveVector)
    {
        if (_onHitCoroutine != null)
            return;

        spriteRenderer.flipX = moveVector.x < 0;
        transform.position += moveVector;
    }

    protected void OnAnimate(AnimationType type)
    {
        switch (type)
        {
            default:
            case AnimationType.IDLE:
                animator.SetTrigger(Idle);
                break;
            case AnimationType.WALK:
                animator.SetTrigger(Walk);
                break;
            case AnimationType.HIT:
                animator.SetTrigger(Hit);
                break;
            case AnimationType.DEATH:
                animator.SetTrigger(Death);
                break;
        }
    }
}

public enum AnimationType
{
    IDLE,
    WALK,
    HIT,
    DEATH
}
