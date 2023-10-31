using System.Collections.Generic;
using UnityEngine;

using GUIs.Panels;
using GUIs.Windows;

namespace GUIs
{
    public class GUIManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private IngamePanel ingamePanel;
    
        [Header("Windows")]
        [SerializeField] private MenuWindow menuWindow;
        [SerializeField] private PauseWindow pauseWindow;
        [SerializeField] private GameOverWindow gameOverWindow;
        [SerializeField] private InfoWindow infoWindow;

        private HashSet<Window> openedWindows = new HashSet<Window>();
        private HashSet<Panel> openedPanels = new HashSet<Panel>();
    
    

        public T GetPanel<T>() where T : class 
        {
            switch (typeof(T).ToString()) {
                default:
                case "GUIs.Panels.IngamePanel":
                    return ingamePanel as T;
            }
        }

        public T GetWindow<T>() where T : class 
        {
            switch (typeof(T).ToString()) {
                default:
                case "GUIs.Windows.MenuWindow":
                    return menuWindow as T;
                case "GUIs.Windows.PauseWindow":
                    return pauseWindow as T;
                case "GUIs.Windows.GameOverWindow":
                    return gameOverWindow as T;
                case "GUIs.Windows.InfoWindow":
                    return infoWindow as T;
            }
        }

        public void SetWindowOpened(Window window) => openedWindows.Add(window);
        public void SetWindowClosed(Window window) => openedWindows.Remove(window);

        public void SetPanelOpened(Panel panel) => openedPanels.Add(panel);
        public void SetPanelClosed(Panel panel) => openedPanels.Remove(panel);
    }
}
