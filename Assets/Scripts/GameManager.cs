using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enter point for game
[RequireComponent(
    typeof(PlayerController),
    typeof(EntityController),
    typeof(FruitController)
)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private PlayerController playerController; 
    private EntityController entityController;
    private FruitController fruitController;
    private FileManager fileManager = new FileManager();
    private int _points = 0;


    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerController = GetComponent<PlayerController>();
        entityController = GetComponent<EntityController>();
        fruitController = GetComponent<FruitController>();
        
        Utilities.UpdateStagePositions();
        
        entityController.Init();
        fruitController.Init();
    }

    private void OnEnable()
    {
        playerController.AddListener(entityController.Player.OnMove);
    }

    private void OnDisable()
    {
        playerController.RemoveAllListeners();
    }

    private void FixedUpdate()
    {
        var deltaTime = Time.deltaTime;
        
        entityController.CustomUpdate(deltaTime);
    }

    public void AddPoints(int value)
    {
        if (value > 0)
            _points += value;
    }
}