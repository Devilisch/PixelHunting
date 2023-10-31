using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using GUIs.Windows;
using Managers;
using Models;

namespace GUIs.Panels
{
    public class IngamePanel : Panel
    {
        [SerializeField] private Text healthLabel;
        [SerializeField] private Text pointsLabel;
        [SerializeField] private Button pauseButton;

    

        private void OnEnable()
        {
            GameManager.Instance.PlayerController.Model.AddListener(UpdateHealthLabel);
            GameManager.Instance.ScoreController.AddListener(UpdatePointsLabel);
        
            pauseButton.onClick.AddListener(() => GameManager.Instance.GUIManager.GetWindow<PauseWindow>().Show());
        }

        private void OnDisable()
        {
            GameManager.Instance.PlayerController.Model.RemoveListener(UpdateHealthLabel);
            GameManager.Instance.ScoreController.RemoveListener(UpdatePointsLabel);
        
            pauseButton.onClick.RemoveAllListeners();
        }

        private void UpdateHealthLabel(PlayerModel playerModel) => healthLabel.text = playerModel.Health.ToString();
        private void UpdatePointsLabel(int value) => pointsLabel.text = value.ToString();
    }
}
