using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private EntityScriptableObject ratScriptableObject;
    [SerializeField] private Transform spawnTransform;
    [SerializeField, Range(0,100)] private int maxCount = 5;
    [SerializeField, Range(0,60)] private float moveTimer = 5;

    private float time = 0;
    private bool isGameStart = false;

    public ObjectsPool<RatController> Rats { get; private set; }



    public void Init()
    {
        Rats = new ObjectsPool<RatController>(
            Initialize,
            Get,
            Return,
            maxCount
        );
    }

    public void CustomUpdate(float deltaTime)
    {
        if (!isGameStart)
            return;
        
        time += deltaTime;

        foreach (var rat in Rats.ActiveObjects)
        {
            if (time > moveTimer)
                rat.OnMove(Utilities.GetRandomStagePositions());
            
            rat.CustomUpdate(deltaTime);
        }
        
        if (time > moveTimer)
            time = 0;

        UpdateRats();
    }

    private void UpdateRats()
    {
        while (Rats.ActiveCount < maxCount)
            Spawn();
    }

    public void OnGameStart()
    {
        isGameStart = true;
    }

    public void OnGameEnd()
    {
        isGameStart = false;
        Rats.ReturnAll();
    }
    
    private void Spawn() => Rats.Get().OnSpawn();
    
    private RatController Initialize()
    {
        var newRatController = new RatController();
        
        newRatController.Init(enemyPrefab, ratScriptableObject, spawnTransform);

        return newRatController;
    }
    
    private void Get(RatController ratController) => ratController.View.Show();
    private void Return(RatController ratController) => ratController.View.Hide();
}
