using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Rat ratPrefab;
    [SerializeField] private Transform entitiesTransform;
    [SerializeField, Range(0,100)] private int ratCount = 5;
    [SerializeField, Range(0,60)] private float ratMovementTimer = 5;

    private float _time = 0;

    public Player Player { get; private set; }
    public HashSet<Rat> Rats { get; private set; } = new HashSet<Rat>();



    public void Init()
    {
        _time = ratMovementTimer;
        
        Player = Instantiate(playerPrefab, entitiesTransform);
        Player.Init();

        for (int i = 0; i < ratCount; i++)
        {
            var newRat = Instantiate(ratPrefab, entitiesTransform);

            newRat.Init();
            newRat.transform.position = Utilities.GetRandomStagePositions();
            Rats.Add(newRat);
        }
        Debug.Log(1);
    }

    public void CustomUpdate(float deltaTime)
    {
        Debug.Log(2);
        _time += deltaTime;

        Player.CustomUpdate(deltaTime);

        foreach (var rat in Rats)
        {
            if (_time > ratMovementTimer)
                rat.OnMove(Utilities.GetRandomStagePositions());
            
            rat.CustomUpdate(deltaTime);
        }
        
        if (_time > ratMovementTimer)
            _time = 0;
    }
}
