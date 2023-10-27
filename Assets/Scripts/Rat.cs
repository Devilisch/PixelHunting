using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Entity
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        OnHitCoroutine ??= StartCoroutine(OnHit());
        col.gameObject.GetComponent<IDamageable>()?.OnDamage();
    }
}
