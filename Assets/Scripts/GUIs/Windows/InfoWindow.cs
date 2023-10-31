using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace GUIs.Windows
{
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
}
