using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Rat ratPrefab;
    [SerializeField] private Transform entitiesTransform;
    [SerializeField, Range(0,100)] private int ratCount = 5;
    [SerializeField, Range(0,60)] private float ratMovementTimer = 5;

    private float _time = 0;
    private Vector3 _leftBottomPosition;
    private Vector3 _rightTopPosition;

    public Player Player { get; private set; }
    public List<Rat> Rats { get; private set; } = new List<Rat>();



    public void Init()
    {
        var mainCamera = Camera.main;

        if (mainCamera != null)
        {
            _leftBottomPosition = mainCamera.ScreenToWorldPoint(Vector3.zero);
            _rightTopPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        }
            
        Player = Instantiate(playerPrefab, entitiesTransform);

        for (int i = 0; i < ratCount; i++)
        {
            var newRat = Instantiate(ratPrefab, entitiesTransform);

            newRat.transform.position = RandomPosition();
            newRat.OnMove(RandomPosition());
            Rats.Add(newRat);
        }
    }

    public void CustomUpdate(float deltaTime)
    {
        _time += deltaTime;
        
        Player.CustomUpdate(deltaTime);

        Rats.ForEach((rat) => rat.CustomUpdate(deltaTime));

        if (_time < ratMovementTimer) 
            return;
        
        Rats.ForEach((rat) => rat.OnMove(RandomPosition()));
        _time = 0;
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(
            Random.Range(_leftBottomPosition.x, _rightTopPosition.x),
            Random.Range(_leftBottomPosition.y, _rightTopPosition.y)
        );
    }
}
