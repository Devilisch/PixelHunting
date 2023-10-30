using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private IngamePanel ingamePanel;
    
    [Header("Windows")]
    [SerializeField] private MenuWindow menuWindow;
    [SerializeField] private PauseWindow pauseWindow;
    // [SerializeField] private GameOverWindow buffWindow;
    // [SerializeField] private InfoWindow pauseWindow;
    // [SerializeField] private GameOverWindow gameOverWindow;

    private HashSet<Window> _openedWindows = new HashSet<Window>();
    private HashSet<Panel> _openedPanels = new HashSet<Panel>();
    
    

    public T ShowPanel<T>() where T : class 
    {
        switch (typeof(T).ToString()) {
            default:
            case "IngamePanel":
                return ingamePanel.Show() as T;
        }
    }

    public T ShowWindow<T>() where T : class 
    {
        switch (typeof(T).ToString()) {
            default:
            case "MenuWindow":
                return menuWindow.Show() as T;
            case "PauseWindow":
                return pauseWindow.Show() as T;
        }
    }

    public void SetWindowOpened(Window window) => _openedWindows.Add(window);
    public void SetWindowClosed(Window window) => _openedWindows.Remove(window);

    public void SetPanelOpened(Panel panel) => _openedPanels.Add(panel);
    public void SetPanelClosed(Panel panel) => _openedPanels.Remove(panel);
}
