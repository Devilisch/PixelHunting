using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace GUIs.Windows
{
    public class MenuWindow : Window
    {
        [SerializeField] private Text highScoreText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button infoButton;
        [SerializeField] private Button exitButton;


        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            infoButton.onClick.AddListener(OnInfoButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);

            highScoreText.text = "HIGHSCORE: " + GameManager.Instance.ScoreController.Model.HighScore;
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
            infoButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }

        private void OnPlayButtonClicked()
        {
            GameManager.Instance.GameplayController.OnGameStart();
            Hide();
        }

        private void OnInfoButtonClicked()
        {
            GameManager.Instance.GUIManager.GetWindow<InfoWindow>().Show();
        }

        private void OnExitButtonClicked() => Application.Quit();
    }
}
