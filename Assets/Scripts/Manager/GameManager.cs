using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enter point for game
[RequireComponent(
    typeof(EnemyManager),
    typeof(FruitManager)
)]
public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerView playerPrefab;
    [SerializeField] private EntityScriptableObject playerScriptableObject;
    [SerializeField] private Transform playerSpawnTransform;
    
    [Header("Enemies")]
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private List<EntityScriptableObject> enemies;
    [SerializeField] private Transform enemiesSpawnTransform;
    
    [Header("Fruits")]
    [SerializeField] private FruitView fruitPrefab;
    [SerializeField] private List<EntityScriptableObject> fruits;
    [SerializeField] private Transform fruitsSpawnTransform;
    
    public static GameManager Instance { get; private set; }
    public PlayerController PlayerController { get; private set; } = new PlayerController();
    
    private EnemyManager enemyManager;
    private FruitManager fruitManager;
    private TouchManager touchManager;
    private FileManager fileManager = new FileManager();
    private int _points = 0;


    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        enemyManager = GetComponent<EnemyManager>();
        fruitManager = GetComponent<FruitManager>();
        touchManager = GetComponent<TouchManager>();
        
        Utilities.UpdateStagePositions();
        
        PlayerController.Init(playerPrefab, playerScriptableObject, playerSpawnTransform);
        enemyManager.Init();
        fruitManager.Init();
        
        touchManager.AddListener(PlayerController.OnMove);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        
        PlayerController.CustomUpdate(deltaTime);
        enemyManager.CustomUpdate(deltaTime);
        touchManager.CustomUpdate(deltaTime);
    }
}