using System;
using UnityEngine;

namespace Managers
{
    public class TouchManager : MonoBehaviour
    {
        private Action<Vector3> onTouchAction;


    
        public void CustomUpdate(float deltaTime)
        {
            // for Windows testing
            if (!Input.GetButtonDown("Fire1"))
                return;
            
            if (Camera.main != null &&
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit0, 10))
            {
                // Debug.Log("Did Hit " + hit.point);
                onTouchAction?.Invoke(new Vector3(hit0.point.x, hit0.point.y, 0));
            }
            else
            {
                // Debug.Log("Did not Hit");
            }
        
            // // for Android
            // if (Input.touchCount == 0)
            //     return;
            //
            // if (Camera.main != null &&
            //     Physics.Raycast(
            //         Camera.main.ScreenPointToRay(
            //             Input.touches[Input.touchCount - 1].position), 
            //             out var hit, 
            //             10
            //         )
            //     )
            //     onTouchAction?.Invoke(new Vector3(hit.point.x, hit.point.y, 0));
        }
    
        public void AddListener(Action<Vector3> action) => onTouchAction += action;
        public void RemoveListener(Action<Vector3> action) => onTouchAction -= action;
    }
}
