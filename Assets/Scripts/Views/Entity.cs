using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Animator),typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    
    protected Coroutine OnHitCoroutine = null;
    
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Vector3 _moveToPoint = Vector3.zero;
    private bool _isIdle = true;
    
    private const float ON_HIT_PAUSE = 0.3f;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");


    
    public void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        OnIdle();
    }
    
    protected IEnumerator OnHit()
    {
        OnAnimate(AnimationType.HIT);

        yield return new WaitForSeconds(ON_HIT_PAUSE);

        OnHitCoroutine = null;
    }
    
    public void OnMove(Vector3 newPosition)
    {
        _isIdle = false;
        OnAnimate(AnimationType.WALK);
        
        if (newPosition == _moveToPoint)
            return;

        _spriteRenderer.flipX = (newPosition.x - transform.position.x) < 0;
        _moveToPoint = newPosition;
    }

    private void OnIdle()
    {
        _isIdle = true;
        OnAnimate(AnimationType.IDLE);

        transform.position = _moveToPoint;
    }

    public void CustomUpdate(float deltaTime)
    {
        if (OnHitCoroutine != null || _isIdle)
            return;

        transform.position += (_moveToPoint - transform.position).normalized * speed * deltaTime;

        if ((_moveToPoint - transform.position).magnitude < 0.1f)
            OnIdle();
    }

    protected void OnAnimate(AnimationType type, bool state = true)
    {
        if (name.StartsWith("Rat"))
            Debug.Log(type);
        
        switch (type)
        {
            default:
            case AnimationType.IDLE:
                SetAnimation(Idle);
                break;
            case AnimationType.WALK:
                SetAnimation(Walk, state);
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
        SetAnimation(Walk, false);
        _animator.SetTrigger(nameHash);
    }

    private void SetAnimation(int nameHash, bool state)
    {
        _animator.SetBool(nameHash, state);
    }
}

public enum AnimationType
{
    IDLE,
    WALK,
    HIT,
    DEATH
}
