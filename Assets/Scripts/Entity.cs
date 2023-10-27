using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Animator),typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    protected Coroutine OnHitCoroutine = null;
    
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _onHitPause = 0.25f;
    
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");



    public void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    protected IEnumerator OnHit()
    {
        OnAnimate(AnimationType.HIT);

        yield return new WaitForSeconds(_onHitPause);

        OnHitCoroutine = null;
    }
    
    public void OnMove(Vector2 velocity)
    {
        if (OnHitCoroutine != null)
            return;

        _spriteRenderer.flipX = velocity.x < 0;
        _rigidbody.velocity = velocity;
    }

    protected void OnAnimate(AnimationType type)
    {
        switch (type)
        {
            default:
            case AnimationType.IDLE:
                SetAnimation(Idle);
                break;
            case AnimationType.WALK:
                SetAnimation(Walk);
                break;
            case AnimationType.HIT:
                SetAnimation(Hit);
                break;
            case AnimationType.DEATH:
                SetAnimation(Death);
                break;
        }
    }

    private void SetAnimation(int nameHash)
    {
        if (_animator.parameters.Any(parameter => parameter.nameHash == nameHash))
            _animator.SetTrigger(nameHash);
    }
}

public enum AnimationType
{
    IDLE,
    WALK,
    HIT,
    DEATH
}
