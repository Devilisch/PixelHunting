using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Player touch " + col.gameObject.name);
        
        if (_onHitCoroutine == null)
            _onHitCoroutine = StartCoroutine(OnHit());
    }
}
