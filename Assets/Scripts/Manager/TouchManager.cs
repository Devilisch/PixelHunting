using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TouchManager : MonoBehaviour
{
    private Action<Vector3> _onTouchAction;


    
    public void CustomUpdate(float deltaTime)
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        if (Camera.main != null &&
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 10))
        {
            Debug.Log("Did Hit " + hit.point);
            _onTouchAction?.Invoke(new Vector3(hit.point.x, hit.point.y, 0));
        }
        else
        {
            Debug.Log("Did not Hit");
        }
        
    }
    
    public void AddListener(Action<Vector3> action) => _onTouchAction += action;
    public void RemoveListener(Action<Vector3> action) => _onTouchAction -= action;
}
