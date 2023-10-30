using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : Window
{
    [SerializeField] private Text scoreTest;
    [SerializeField] private Button exitButton;


    private void OnEnable()
    {
        exitButton.onClick.AddListener(OnExitButtonClicked);

        scoreTest.text = "HIGHSCORE: " + GameManager.Instance.ScoreController.Points;
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
