using UnityEngine;

using Managers;
using Interfaces;

namespace GUIs.Windows
{
    public class Window : MonoBehaviour, IGUI
    {
        public void Show()
        {
            GameManager.Instance.GUIManager.SetWindowOpened(this);
            gameObject.SetActive(true);
        }
    
        public void Hide()
        {
            GameManager.Instance.GUIManager.SetWindowClosed(this);
            gameObject.SetActive(false);
        }
    }
}
