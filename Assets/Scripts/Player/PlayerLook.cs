using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerLook : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData playerData;

    [Header("Scene References")]
    [SerializeField] private Camera playerCamera;

    private Vector2 lookInput;
    private float mouseX;
    private float mouseY;
    private float xRotation;

    private void Awake()
    {
        if (playerData != null)
            Cursor.visible = playerData.isCursorVisible;
    }

    private void OnEnable()
    {
        InputManager.Actions.Game.Look.performed += OnLook;
    }

    private void OnDisable()
    {
        InputManager.Actions.Game.Look.performed -= OnLook;
    }

    public void OnLook(InputAction.CallbackContext obj)
    {
        lookInput = obj.ReadValue<Vector2>();
        UpdateLook();
    }

    protected virtual void UpdateLook()
    {
        if (playerData == null) return;

        mouseX = lookInput.x * playerData.horizontalSensitivity * Time.deltaTime;
        mouseY = lookInput.y * playerData.verticalSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}