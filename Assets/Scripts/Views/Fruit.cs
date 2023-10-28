using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Animator),typeof(Collider2D))]
public class Fruit : MonoBehaviour, ITakeable
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Action<Fruit> _takeAction;
    
    public FruitInfo Info { get; private set; }



    public void Init(FruitInfo fruitInfo)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        Info = fruitInfo;
        _spriteRenderer.sprite = Info.Sprite;
        _animator.runtimeAnimatorController = Info.AnimatorController;
        name = Info.Type.ToString();
    }
    
    public void OnTake() =>  _takeAction?.Invoke(this);

    public void AddListener(Action<Fruit> action) => _takeAction += action;
    public void RemoveListener(Action<Fruit> action) => _takeAction -= action;
    public void RemoveAllListeners() => _takeAction = null;
}

public enum FruitType
{
    APPLE,
    ORANGE,
    KIWI
}
