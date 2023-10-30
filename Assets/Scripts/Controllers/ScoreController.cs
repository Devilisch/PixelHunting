using System;
using Defective.JSON;
using UnityEngine;

public class ScoreController
{
    private JSONObject statisticsJson;
    private Action<int> updateScoreAction;
    
    public int Points { get; private set; } = 0;
    public int HighScore { get; private set; } = 1;

    
    
    public void AddPoints(EntityType type)
    {
        switch (type)
        {
            default:
            case EntityType.APPLE:
                Points += 1;
                break;
            case EntityType.ORANGE:
                Points += 5;
                break;
            case EntityType.KIWI:
                Points += 10;
                break;
        }
        
        updateScoreAction?.Invoke(Points);
    }

    public void Reset()
    {
        Points = 0;
        
        updateScoreAction?.Invoke(Points);
    } 

    public void Save()
    {
        if (statisticsJson.HasField("HighScore"))
            statisticsJson["HighScore"].intValue = HighScore;

        FileManager.SaveStatistics(statisticsJson.ToString());
    }

    public void Load()
    {
        statisticsJson = new JSONObject(FileManager.LoadStatistics());

        if (statisticsJson.HasField("HighScore"))
            HighScore = statisticsJson["HighScore"].intValue;
    }
    
    public void AddListener(Action<int> action) => updateScoreAction += action;
    public void RemoveListener(Action<int> action) => updateScoreAction -= action;
}
