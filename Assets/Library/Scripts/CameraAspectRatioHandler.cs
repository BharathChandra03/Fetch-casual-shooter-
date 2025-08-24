using UnityEngine;

public class CameraAspectRatioHandler : MonoBehaviour
{
    [SerializeField] private float baseOrthoSize = 5f; // Base orthographic size for the camera
    [SerializeField] private Vector2 baseAspectRatio = new Vector2(16f, 9f); // Base aspect ratio (width, height)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float targetAspectRatio = baseAspectRatio.x / baseAspectRatio.y;
        float currentAspectRatio = (float)Screen.width / Screen.height;

        Camera.main.orthographicSize= baseOrthoSize * (currentAspectRatio / targetAspectRatio);
    }

}
