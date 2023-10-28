using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Entity
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        transform.position -= transform.position - col.transform.position;
        OnHitCoroutine ??= StartCoroutine(OnHit());
        col.gameObject.GetComponent<IDamageable>()?.OnDamage();
    }
}
