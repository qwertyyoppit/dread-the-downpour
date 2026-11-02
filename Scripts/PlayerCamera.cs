using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform Followed;

    public Vector3 Offset;
    public float Distance;
    public float Sensitivity = 10.0f;

    private float yaw;
    private float pitch;

    private InputSystem_Actions input;

    private void Awake()
    {
        input = new();
    }

    private void OnEnable()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        input.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        input.Player.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        var look = input.Player.Look.ReadValue<Vector2>();

        var scale = Time.deltaTime * Sensitivity;

        yaw += look.x * scale;
        pitch -= look.y * scale;
        pitch = Mathf.Clamp(pitch, -89.0f, 89.0f);

        transform.eulerAngles = new Vector3(pitch, yaw);
        transform.position = Followed.position + Offset + -(transform.forward * Distance);
    }
}
