using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : Window
{
    [SerializeField] private Text informationText;
    [SerializeField] private Button closeButton;


    private void OnEnable()
    {
        informationText.text = FileManager.LoadTargets();
        
        closeButton.onClick.AddListener(() => Hide());
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
    }
}
