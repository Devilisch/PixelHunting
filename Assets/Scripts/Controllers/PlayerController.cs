using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class PlayerController : MonoBehaviour, IPointerDownHandler
{
    private Action<Vector3> _onTapAction;
    
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        var mainCamera = Camera.main;

        if (mainCamera != null) 
            _onTapAction?.Invoke(mainCamera.ScreenToWorldPoint(eventData.position) - mainCamera.transform.position);
    }

    public void AddListener(Action<Vector3> action) => _onTapAction += action;
    public void RemoveListener(Action<Vector3> action) => _onTapAction -= action;
    public void RemoveAllListeners() => _onTapAction = null;
}
