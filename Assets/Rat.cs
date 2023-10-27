using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Entity
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_onHitCoroutine == null)
            _onHitCoroutine = StartCoroutine(OnHit());
    }
}
