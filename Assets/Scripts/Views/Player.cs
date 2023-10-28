using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDamageable
{
    private bool _isActive = true;
    private Action _onDamageAction;


    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Player touch " + col.gameObject.name);
        
        col.gameObject.GetComponent<ITakeable>()?.OnTake();
    }

    public void OnDamage()
    {
        if (!_isActive)
            return;
        
        if (OnHitCoroutine == null)
            OnHitCoroutine = StartCoroutine(OnHit());
        
        _onDamageAction?.Invoke();
    }

    public void OnDeath()
    {
        _isActive = false;
        OnAnimate(AnimationType.DEATH);
    }

    public void AddDamageListener(Action action) => _onDamageAction += action;
    public void RemoveDamageListener(Action action) => _onDamageAction -= action;
    public void RemoveDamageAllListeners() => _onDamageAction = null;
}
