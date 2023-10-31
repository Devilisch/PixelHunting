using GUIs.Panels;
using GUIs.Windows;
using Managers;

namespace Controllers
{
    public class GameplayController
    {
        private Panel panel;
        private Window window;



        public void Init()
        {
            panel = GameManager.Instance.GUIManager.GetPanel<IngamePanel>();
            window = GameManager.Instance.GUIManager.GetWindow<MenuWindow>();
        }
    
        public void OnGameStart()
        {
            GameManager.Instance.IsUpdateActive = true;
        
            GameManager.Instance.PlayerController.OnSpawn();
            GameManager.Instance.EnemyManager.OnGameStart();
            GameManager.Instance.FruitManager.OnGameStart();

            panel.Show();
            window.Hide();
        
            GameManager.Instance.ScoreController.Reset();
        }

        public void OnGameEnd()
        {
            GameManager.Instance.IsUpdateActive = false;
        
            GameManager.Instance.EnemyManager.OnGameEnd();
            GameManager.Instance.FruitManager.OnGameEnd();
            GameManager.Instance.ScoreController.UpdateHighScore();
        
            panel.Hide();
            window.Show();
        }

        public void OnPlayerDeath() => GameManager.Instance.GUIManager.GetWindow<GameOverWindow>().Show();
    }
}
