using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public Window Show()
    {
        GameManager.Instance.GUIManager.SetWindowOpened(this);
        gameObject.SetActive(true);

        return this;
    }
    
    public Window Hide()
    {
        GameManager.Instance.GUIManager.SetWindowClosed(this);
        gameObject.SetActive(false);

        return this;
    }
}
