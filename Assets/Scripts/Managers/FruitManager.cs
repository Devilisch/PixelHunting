using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Controllers;
using ScriptableObjects;
using Views;

namespace Managers
{
    public class FruitManager : MonoBehaviour
    {
        [SerializeField] private List<FruitSettings> fruitsSettings;
        [SerializeField] private FruitView prefab;
        [SerializeField] private Transform fruitsTransform;
    
        private Dictionary<EntityType, ObjectsPool<FruitController>> fruitPools = new Dictionary<EntityType, ObjectsPool<FruitController>>();
        private bool isGameStart = false;
    
    
    
        public void Init()
        {
            fruitsSettings
                .ForEach(
                    (fruitSetting) => 
                        fruitPools.Add(
                            fruitSetting.info.Type,
                            new ObjectsPool<FruitController>(
                                () => Initialize(fruitSetting.info),
                                Get,
                                Return,
                                fruitSetting.count
                            )
                        )
                );
        
            UpdateFruits();
        }
    
        private void UpdateFruits()
        {
            foreach (var fruitPool in fruitPools)
            {
                var maxCount = fruitsSettings
                    .First((setting) => setting.info.Type == fruitPool.Key).count;
                
                while (fruitPool.Value.ActiveCount < maxCount)
                    Spawn(fruitPool.Key);
            }
        }
    
        public void TakeFruit(FruitController fruit)
        {
            fruitPools[fruit.View.Type].Return(fruit);
    
            UpdateFruits();
        }

        public void OnGameStart()
        {
            isGameStart = true;
            UpdateFruits();
        }

        public void OnGameEnd()
        {
            isGameStart = false;
        
            foreach(var pool in fruitPools.Values)
                pool.ReturnAll();
        }
    
        private FruitController Initialize(EntityScriptableObject entityScriptableObject)
        {
            var newFruitController = new FruitController();
        
            newFruitController.Init(prefab, entityScriptableObject, fruitsTransform);

            return newFruitController;
        }
    
        private void Spawn(EntityType type) 
            => fruitPools[type].Get().View.transform.position = Utilities.GetRandomStagePositions();
        private void Get(FruitController fruitController) => fruitController.Show();
        private void Return(FruitController fruitController) => fruitController.Hide();
    }

    [Serializable]
    public class FruitSettings
    {
        public EntityScriptableObject info;
        [Range(0, 100)] public int count;
    }
}