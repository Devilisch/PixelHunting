using UnityEngine;
using System.Threading;

using Controllers;
using GUIs;
using GUIs.Windows;
using ScriptableObjects;
using Views;

// Enter point for game
namespace Managers
{
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

        [Header("Test object")]
        [SerializeField] private Material testMaterial;
        
    
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
            
            Application.targetFrameRate = Screen.currentResolution.refreshRate; 

            EnemyManager = enemyManager;
            FruitManager = fruitManager;
            TouchManager = touchManager;
            GUIManager = guiManager;
        
            Utilities.UpdateStagePositions();
        
            PlayerController.Init(playerPrefab, playerScriptableObject, playerSpawnTransform);
            ScoreController.Init();
            GameplayController.Init();
            EnemyManager.Init();
            FruitManager.Init();
        
            TouchManager.AddListener(PlayerController.OnMove);
            GUIManager.GetWindow<MenuWindow>().Show();

            // var testThread = new Thread(TestThread);
            // testThread.Start();
        }
        
        private void Update()
        {
            if(!IsUpdateActive)
                return;
        
            var deltaTime = Time.deltaTime;
            
            // testMaterial.mainTextureOffset = new Vector2(Random.Range(-0.99f,0.99f),Random.Range(-0.99f,0.99f));
        
            PlayerController.CustomUpdate(deltaTime);
            EnemyManager.CustomUpdate(deltaTime);
            TouchManager.CustomUpdate(deltaTime);
        }


        private void TestThread()
        {
            var deltaTime = 0f;
            
            while (deltaTime < 1000000)
                Debug.Log(deltaTime++);
        }
    }
}