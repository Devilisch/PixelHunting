using System;
using Defective.JSON;

using Managers;

namespace Controllers
{
    public class ScoreController
    {
        private Action<int> updateScoreAction;
        public ScoreModel Model { get; private set; }



        public void Init()
        {
            Model = new ScoreModel();
        }
        
        public void AddPoints(EntityType type)
        {
            switch (type)
            {
                default:
                case EntityType.APPLE:
                    Model.AddPoints(1);
                    break;
                case EntityType.ORANGE:
                    Model.AddPoints(5);
                    break;
                case EntityType.KIWI:
                    Model.AddPoints(10);
                    break;
            }
        
            updateScoreAction?.Invoke(Model.Score);
        }

        public void Reset()
        {
            Model.Reset();
        
            updateScoreAction?.Invoke(Model.Score);
        }

        public void UpdateHighScore() => Model.SaveHighScore();
    
        public void AddListener(Action<int> action) => updateScoreAction += action;
        public void RemoveListener(Action<int> action) => updateScoreAction -= action;
    }
}

public class ScoreModel
{
    private JSONObject statisticsJson;
    
    private int highScore = -1;
    
    public int Score { get; private set; } = 0;

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

    public void Save()
    {
        if (statisticsJson != null && statisticsJson.HasField("HighScore"))
        {
            statisticsJson.SetField("HighScore", highScore);
        }
        else
        {
            statisticsJson = new JSONObject();
            statisticsJson.AddField("HighScore", highScore);
        }

        FileManager.SaveStatistics(statisticsJson.ToString());
    }

    private void Load()
    {
        highScore = 0;
        
        var statisticsString = FileManager.LoadStatistics();
        
        if (string.IsNullOrEmpty(statisticsString))
            return;
            
        statisticsJson = new JSONObject(statisticsString);

        if (statisticsJson.HasField("HighScore"))
            highScore = statisticsJson["HighScore"].intValue;
    }

    public void AddPoints(int value)
    {
        if (value > 0)
            Score += value;
    }

    public void SaveHighScore() => HighScore = Score;

    public void Reset() => Score = 0;
}
