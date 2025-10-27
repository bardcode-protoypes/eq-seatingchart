using UnityEngine;
using UnityEngine.InputSystem; // <-- new system namespace

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Pan Settings")]
    [SerializeField] private float mousePanSpeed = 0.005f;
    [SerializeField] private float keyboardPanSpeed = 10f;

    [Header("Zoom Settings")]
    [SerializeField] private float mouseZoomSpeed = 500f;
    [SerializeField] private float keyboardZoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 15f;

    private Camera cam;
    private Vector2 lastMousePosition;
    private bool isPanning = false;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleMousePan();
        HandleKeyboardPan();
        HandleMouseZoom();
        HandleKeyboardZoom();
    }

    private void HandleMousePan()
    {
        // Check right mouse held
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            lastMousePosition = Mouse.current.position.ReadValue();
            isPanning = true;
        }

        if (Mouse.current.rightButton.isPressed && isPanning)
        {
            Vector2 mouseDelta = Mouse.current.position.ReadValue() - lastMousePosition;
            Vector3 move = new Vector3(-mouseDelta.x, -mouseDelta.y, 0) * mousePanSpeed;
            transform.Translate(move, Space.Self);
            lastMousePosition = Mouse.current.position.ReadValue();
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isPanning = false;
        }
    }

    private void HandleKeyboardPan()
    {
        Vector2 move = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) move.y += 1;
        if (Keyboard.current.sKey.isPressed) move.y -= 1;
        if (Keyboard.current.aKey.isPressed) move.x -= 1;
        if (Keyboard.current.dKey.isPressed) move.x += 1;

        transform.Translate(move * keyboardPanSpeed * Time.deltaTime, Space.Self);
    }

    private void HandleMouseZoom()
    {
        float zoomDelta = 0f;
        
        zoomDelta += Mouse.current.scroll.ReadValue().y * 0.1f;

        cam.orthographicSize -= zoomDelta * mouseZoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }
    
    private void HandleKeyboardZoom()
    {
        float zoomDelta = 0f;

        if (Keyboard.current.qKey.isPressed) zoomDelta -= 1f;
        if (Keyboard.current.eKey.isPressed) zoomDelta += 1f;

        cam.orthographicSize -= zoomDelta * keyboardZoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }
}
