using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Entity
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        transform.position -= transform.position - col.transform.position;

        var target = col.gameObject.GetComponent<IDamageable>();

        if (target == null)
            return;
        
        OnHitCoroutine ??= StartCoroutine(OnHit());
        target.OnDamage();
    }
}
