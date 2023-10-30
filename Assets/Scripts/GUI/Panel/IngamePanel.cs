using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IngamePanel : Panel
{
    [SerializeField] private Text healthLabel;
    [SerializeField] private Text pointsLabel;
    [SerializeField] private Button pauseButton;

    

    private void OnEnable()
    {
        GameManager.Instance.PlayerController.Model.AddListener(
            (playerModel) => UpdateHealthLabel(playerModel.Health)
        );
        
        GameManager.Instance.ScoreController.AddListener(UpdatePointsLabel);
        
        pauseButton.onClick.AddListener(() => GameManager.Instance.GUIManager.ShowWindow<PauseWindow>());
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerController.Model.RemoveListener(
            (playerModel) => UpdateHealthLabel(playerModel.Health)
        );
        
        GameManager.Instance.ScoreController.RemoveListener(UpdatePointsLabel);
        
        pauseButton.onClick.RemoveAllListeners();
    }

    public void UpdateHealthLabel(int value) => healthLabel.text = value.ToString();
    public void UpdatePointsLabel(int value) => pointsLabel.text = value.ToString();
    public void AddListener(UnityAction action) => pauseButton.onClick.AddListener(action);
    public void RemoveListener(UnityAction action) => pauseButton.onClick.RemoveListener(action);
}
