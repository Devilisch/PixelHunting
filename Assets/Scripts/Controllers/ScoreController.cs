using System;
using Defective.JSON;
using UnityEngine;

public class ScoreController
{
    private int highScore = -1;
    private JSONObject statisticsJson;
    private Action<int> updateScoreAction;
    
    public int Points { get; private set; } = 0;

    public int HighScore
    {
        get
        {
            if (highScore < 0)
                Load();

            return highScore;
        }

        private set
        {
            if (value > highScore)
            {
                highScore = value;
                Save();
            }
        }
    }

    
    
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
            statisticsJson.SetField("HighScore", HighScore);
        else
            statisticsJson.AddField("HighScore", HighScore);

        FileManager.SaveStatistics(statisticsJson.ToString());
    }

    private void Load()
    {
        statisticsJson = new JSONObject(FileManager.LoadStatistics());

        if (statisticsJson.HasField("HighScore"))
            HighScore = statisticsJson["HighScore"].intValue;
    }

    public void UpdateHighScore() => HighScore = Points;
    
    public void AddListener(Action<int> action) => updateScoreAction += action;
    public void RemoveListener(Action<int> action) => updateScoreAction -= action;
}
