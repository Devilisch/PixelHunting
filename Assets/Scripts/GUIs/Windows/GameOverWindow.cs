using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace GUIs.Windows
{
    public class GameOverWindow : Window
    {
        [SerializeField] private Text scoreTest;
        [SerializeField] private Button exitButton;


        private void OnEnable()
        {
            exitButton.onClick.AddListener(OnExitButtonClicked);

            scoreTest.text = "HIGHSCORE: " + GameManager.Instance.ScoreController.Model.Score;
        }

        private void OnDisable()
        {
            exitButton.onClick.RemoveAllListeners();
        }

        private void OnExitButtonClicked()
        {
            GameManager.Instance.GameplayController.OnGameEnd();
            Hide();
        }
    }
}
