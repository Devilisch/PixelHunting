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
    
    private const float ON_HIT_PAUSE = 0.3f;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");


    
    // delete it after
    private void Awake()
    {
        Init();
    }

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

        OnAnimate(
            (transform.position - _moveToPoint).magnitude < 0.1f ? 
                AnimationType.IDLE : AnimationType.WALK
        );

        OnHitCoroutine = null;
    }
    
    public void OnMove(Vector3 position)
    {
        OnAnimate(AnimationType.WALK);

        _spriteRenderer.flipX = (position.x - transform.position.x) < 0;
        _moveToPoint = position;
    }

    private void OnIdle()
    {
        OnAnimate(AnimationType.IDLE);

        transform.position = _moveToPoint;
    }

    public void CustomUpdate(float deltaTime)
    {
        if (OnHitCoroutine != null || transform.position == _moveToPoint || Camera.main == null)
            return;
        
        Debug.Log((transform.position - _moveToPoint).magnitude);

        if ((transform.position - _moveToPoint).magnitude < 0.1f)
            OnIdle();
        else
            transform.position = Vector2.MoveTowards(
                transform.position, 
                _moveToPoint, 
                speed * Time.deltaTime
            );
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
