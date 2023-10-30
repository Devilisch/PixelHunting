using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Panel Show()
    {
        GameManager.Instance.GUIManager.SetPanelOpened(this);
        gameObject.SetActive(true);

        return this;
    }
    
    public Panel Hide()
    {
        GameManager.Instance.GUIManager.SetPanelClosed(this);
        gameObject.SetActive(false);

        return this;
    }
}
