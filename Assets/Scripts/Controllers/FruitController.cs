using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    [SerializeField] private List<FruitSettings> fruitsSettings;
    [SerializeField] private Fruit prefab;
    [SerializeField] private Transform fruitsTransform;

    private Dictionary<FruitType, ObjectsPool<Fruit>> _fruitPools = new Dictionary<FruitType, ObjectsPool<Fruit>>();



    public void Init()
    {
        fruitsSettings
            .ForEach(
                (fruitSetting) => 
                    _fruitPools.Add(
                        fruitSetting.Info.Type,
                        new ObjectsPool<Fruit>(
                            () => Initialize(fruitSetting.Info),
                            Get,
                            Return,
                            fruitSetting.Info.CountOnStage
                        )
                    )
            );
        
        UpdateFruits();
    }

    private void UpdateFruits()
    {
        foreach (var fruitPool in _fruitPools)
        {
            var maxCount = fruitsSettings
                .First((setting) => setting.Info.Type == fruitPool.Key).Count;
                
            while (fruitPool.Value.ActiveCount < maxCount)
                Spawn(fruitPool.Key);
        }
    }
    

    private void TakeFruit(Fruit fruit)
    {
        _fruitPools[fruit.Info.Type].Return(fruit);

        UpdateFruits();
    }
    
    private Fruit Initialize(FruitInfo fruitInfo)
    {
        var newFruit = Instantiate(prefab, fruitsTransform);
        
        newFruit.Init(fruitInfo);
        newFruit.AddListener(TakeFruit);
        
        return newFruit;
    }
    
    private void Spawn(FruitType type) 
        => _fruitPools[type].Get().transform.position = Utilities.GetRandomStagePositions();
    private void Get(Fruit bodyObject) => bodyObject.gameObject.SetActive(true);
    private void Return(Fruit bodyObject) => bodyObject.gameObject.SetActive(false);
}

[Serializable]
public class FruitSettings
{
    public FruitInfo Info;
    [Range(0, 100)] public int Count;
}
