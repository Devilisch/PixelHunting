using UnityEngine;

public static class Utilities
{
    private static Vector3 _leftBottomStagePosition;
    private static Vector3 _rightTopStagePosition;
    
    
    
    public static void UpdateStagePositions()
    {
        var mainCamera = Camera.main;

        if (mainCamera != null)
        {
            _leftBottomStagePosition = mainCamera.ScreenToWorldPoint(Vector3.zero);
            _rightTopStagePosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        }
    }
    
    public static Vector3 GetRandomStagePositions()
    {
        return new Vector3(
            Random.Range(_leftBottomStagePosition.x, _rightTopStagePosition.x),
            Random.Range(_leftBottomStagePosition.y, _rightTopStagePosition.y)
        );
    }
}
