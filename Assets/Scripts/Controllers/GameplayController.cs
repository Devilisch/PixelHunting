public class GameplayController
{
    private Panel panel;
    
    
    
    public void OnGameStart()
    {
        GameManager.Instance.IsUpdateActive = true;
        panel = GameManager.Instance.GUIManager.ShowPanel<IngamePanel>();
        GameManager.Instance.PlayerController.OnSpawn();
        GameManager.Instance.EnemyManager.OnGameStart();
        GameManager.Instance.FruitManager.OnGameStart();
    }

    public void OnGameEnd()
    {
        GameManager.Instance.IsUpdateActive = false;
        panel.Hide();
        GameManager.Instance.EnemyManager.OnGameEnd();
        GameManager.Instance.FruitManager.OnGameEnd();
    }
}
