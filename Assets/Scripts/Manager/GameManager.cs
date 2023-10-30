using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enter point for game
public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private FruitManager fruitManager;
    [SerializeField] private TouchManager touchManager;
    [SerializeField] private GUIManager guiManager;
    
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
    public GameplayController GameplayController { get; private set; } = new GameplayController();
    
    public EnemyManager EnemyManager { get; private set; }
    public FruitManager FruitManager { get; private set; }
    public TouchManager TouchManager { get; private set; }
    public GUIManager GUIManager { get; private set; }
    
    public bool IsUpdateActive { get; set; } = true;
    
    
    private int points = 0;


    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        EnemyManager = enemyManager;
        FruitManager = fruitManager;
        TouchManager = touchManager;
        GUIManager = guiManager;
        
        Utilities.UpdateStagePositions();
        
        PlayerController.Init(playerPrefab, playerScriptableObject, playerSpawnTransform);
        EnemyManager.Init();
        FruitManager.Init();
        
        TouchManager.AddListener(PlayerController.OnMove);
        GUIManager.ShowWindow<MenuWindow>();
    }

    private void Update()
    {
        if(!IsUpdateActive)
            return;
        
        var deltaTime = Time.deltaTime;
        
        PlayerController.CustomUpdate(deltaTime);
        EnemyManager.CustomUpdate(deltaTime);
        TouchManager.CustomUpdate(deltaTime);
    }
}