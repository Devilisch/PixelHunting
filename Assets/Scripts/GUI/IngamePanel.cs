using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IngamePanel : MonoBehaviour
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
    }

    public void UpdateHealthLabel(int value) => healthLabel.text = value.ToString();
    public void UpdatePointsLabel(int value) => pointsLabel.text = value.ToString();
    public void AddListener(UnityAction action) => pauseButton.onClick.AddListener(action);
    public void RemoveListener(UnityAction action) => pauseButton.onClick.RemoveListener(action);
}
