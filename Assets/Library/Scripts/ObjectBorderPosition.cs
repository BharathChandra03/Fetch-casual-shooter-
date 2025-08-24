using UnityEngine;

public class ObjectBorderPosition : MonoBehaviour
{
    public enum Border {Left, Right, Top, Bottom } // Enum to define the border positions
    public Border border; // The border position of the object
    public float offset = 0.1f; //Offset from the border position
    public float zPosition = 0f; // Z position to maintain depth in the scene

    private Camera _mainCamera; // Reference to the main camera

    private void Start()
    {
        _mainCamera = Camera.main; // Ensure the main camera is assigned
        PositionAtBorder();
    }

    void PositionAtBorder()
    {
        Vector3 viewPointPosition = Vector3.zero; // Initialize viewport position

        switch(border)
        {
            case Border.Left:
                viewPointPosition = new Vector3(offset, 0.5f, zPosition);break;
            case Border.Top:
                viewPointPosition = new Vector3(0.5f - offset, 1f - offset, zPosition);break;
            case Border.Right:
                viewPointPosition = new Vector3(1f - offset, 0.5f - offset, zPosition);break;
            case Border.Bottom:
                viewPointPosition = new Vector3(0.5f, offset, zPosition); break;

        }
        Vector3 worldPosition = _mainCamera.ViewportToWorldPoint(viewPointPosition); // Convert viewport position to world position
        transform.position = worldPosition; // Set the object's position to the calculated world position
    }


























































































































































/*public class PlayerBorderPosition : MonoBehaviour
{
    public enum Border { Left, Right, Top, Bottom }
    public Border border;
    public float offsetFromEdge = 0.5f;

    private Camera _mainCam;

    void Start()
    {
        _mainCam = Camera.main;
        PositionPlayer();
    }

    void PositionPlayer()
    {
        Vector3 viewportPos = Vector3.zero;

        switch (border)
        {
            case Border.Left: viewportPos = new Vector3(0, 0.5f, _mainCam.nearClipPlane); break;
            case Border.Right: viewportPos = new Vector3(1, 0.5f, _mainCam.nearClipPlane); break;
            case Border.Top: viewportPos = new Vector3(0.5f, 1, _mainCam.nearClipPlane); break;
            case Border.Bottom: viewportPos = new Vector3(0.5f, 0, _mainCam.nearClipPlane); break;
        }

        Vector3 worldPos = _mainCam.ViewportToWorldPoint(viewportPos);

        if (border == Border.Left || border == Border.Right)
            worldPos.x += (border == Border.Left ? offsetFromEdge : -offsetFromEdge);
        else
            worldPos.y += (border == Border.Bottom ? offsetFromEdge : -offsetFromEdge);

        worldPos.z = transform.position.z;
        transform.position = worldPos;
    }

    // Optional: Update position if screen changes (e.g., rotation)
    void Update()
    {
        if (Screen.orientation != ScreenOrientation.LandscapeLeft)
            PositionPlayer();
    }
}
*/






}
