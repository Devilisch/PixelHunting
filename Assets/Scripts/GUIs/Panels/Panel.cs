using UnityEngine;

using Managers;
using Interfaces;

namespace GUIs.Panels
{
    public class Panel : MonoBehaviour, IGUI
    {
        public void Show()
        {
            GameManager.Instance.GUIManager.SetPanelOpened(this);
            gameObject.SetActive(true);
        }
    
        public void Hide()
        {
            GameManager.Instance.GUIManager.SetPanelClosed(this);
            gameObject.SetActive(false);
        }
    }
}
