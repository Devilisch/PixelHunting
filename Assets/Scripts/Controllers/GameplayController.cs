public class GameplayController
{
    private Panel panel;
    private Window window;



    public void Init()
    {
        panel = GameManager.Instance.GUIManager.ShowPanel<IngamePanel>();
        window = GameManager.Instance.GUIManager.ShowWindow<MenuWindow>();
    }
    
    public void OnGameStart()
    {
        GameManager.Instance.IsUpdateActive = true;
        
        GameManager.Instance.PlayerController.OnSpawn();
        GameManager.Instance.EnemyManager.OnGameStart();
        GameManager.Instance.FruitManager.OnGameStart();
        GameManager.Instance.ScoreController.Reset();

        panel.Show();
        window.Hide();
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

    public void OnPlayerDeath() => GameManager.Instance.GUIManager.ShowWindow<GameOverWindow>();
}
