using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private List<FruitSettings> fruitsSettings;
    [SerializeField] private FruitView prefab;
    [SerializeField] private Transform fruitsTransform;
    
    private Dictionary<EntityType, ObjectsPool<FruitController>> _fruitPools = new Dictionary<EntityType, ObjectsPool<FruitController>>();
    
    
    
    public void Init()
    {
        fruitsSettings
            .ForEach(
                (fruitSetting) => 
                    _fruitPools.Add(
                        fruitSetting.Info.Type,
                        new ObjectsPool<FruitController>(
                            () => Initialize(fruitSetting.Info),
                            Get,
                            Return,
                            fruitSetting.Count
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
    
    
    private void TakeFruit(EntityView fruit)
    {
        // _fruitPools[fruit.Info.Type].Return(fruit);
    
        UpdateFruits();
    }
    
    private FruitController Initialize(EntityScriptableObject entityScriptableObject)
    {
        var newFruitController = new FruitController();
        
        newFruitController.Init(prefab, entityScriptableObject, fruitsTransform);

        return newFruitController;
    }
    
    private void Spawn(EntityType type) 
        => _fruitPools[type].Get().View.transform.position = Utilities.GetRandomStagePositions();
    private void Get(FruitController fruitController) => fruitController.Show();
    private void Return(FruitController fruitController) => fruitController.Hide();
}

[Serializable]
public class FruitSettings
{
    public EntityScriptableObject Info;
    [Range(0, 100)] public int Count;
}
