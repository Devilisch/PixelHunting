using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enter point script
public class BehaviourManager : MonoBehaviour
{
    public BehaviourManager Instance { get; private set; }
    
    [SerializeField] private TapManager tapManager;
    [SerializeField] private EntityManager entityManager;


    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        entityManager.Init();
    }

    private void OnEnable()
    {
        tapManager.AddListener(entityManager.Player.OnMove);
    }

    private void OnDisable()
    {
        tapManager.RemoveAllListeners();
    }

    private void FixedUpdate()
    {
        var deltaTime = Time.deltaTime;
        
        entityManager.CustomUpdate(deltaTime);
    }
}