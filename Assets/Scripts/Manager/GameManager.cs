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
    public ScoreController ScoreController { get; private set; } = new ScoreController();
    public EnemyManager EnemyManager { get; private set; }
    public FruitManager FruitManager { get; private set; }
    public TouchManager TouchManager { get; private set; }
    
    
    private int _points = 0;


    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        EnemyManager = GetComponent<EnemyManager>();
        FruitManager = GetComponent<FruitManager>();
        TouchManager = GetComponent<TouchManager>();
        
        Utilities.UpdateStagePositions();
        
        PlayerController.Init(playerPrefab, playerScriptableObject, playerSpawnTransform);
        EnemyManager.Init();
        FruitManager.Init();
        
        TouchManager.AddListener(PlayerController.OnMove);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        
        PlayerController.CustomUpdate(deltaTime);
        EnemyManager.CustomUpdate(deltaTime);
        TouchManager.CustomUpdate(deltaTime);
    }
}