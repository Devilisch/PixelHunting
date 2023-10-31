using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace GUIs.Windows
{
    public class PauseWindow : Window
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;


        private void OnEnable()
        {
            GameManager.Instance.IsUpdateActive = false;
        
            continueButton.onClick.AddListener(OnContinueButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }
    
        private void OnDisable()
        {
            GameManager.Instance.IsUpdateActive = true;
        
            continueButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }

        private void OnContinueButtonClicked()
        {
            Hide();
        }

        private void OnExitButtonClicked()
        {
            GameManager.Instance.GameplayController.OnGameEnd();
            Hide();
        }
    }
}
