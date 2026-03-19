using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData playerData;

    private CharacterController characterController;

    public bool IsCrouching { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsSprinting { get; private set; }
    public Vector3 Velocity { get { return characterController.velocity; } }

    private Vector2 movementInput;
    private Vector3 movementVelocity;
    private Vector3 movement;
    private float currentSpeed;
    private float normalHeight;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        normalHeight = characterController.height;

        if (playerData != null)
            currentSpeed = playerData.walkSpeed;
    }

    private void OnEnable()
    {
        InputManager.Actions.Game.Move.performed += OnMove;
        InputManager.Actions.Game.Move.canceled += OnMove;
        InputManager.Actions.Game.Jump.performed += OnJump;
        InputManager.Actions.Game.Crouch.performed += OnCrouch;
        InputManager.Actions.Game.Sprint.performed += OnSprint;
    }

    private void OnDisable()
    {
        InputManager.Actions.Game.Move.performed -= OnMove;
        InputManager.Actions.Game.Move.canceled -= OnMove;
        InputManager.Actions.Game.Jump.performed -= OnJump;
        InputManager.Actions.Game.Crouch.performed -= OnCrouch;
        InputManager.Actions.Game.Sprint.performed -= OnSprint;
    }

    public void OnMove(InputAction.CallbackContext obj)
    {
        movementInput = obj.ReadValue<Vector2>();
    }

    public bool CanJump()
    {
        return characterController.isGrounded;
    }

    public void OnJump(InputAction.CallbackContext obj)
    {
        if (CanJump() && playerData != null)
        {
            movementVelocity.y = playerData.jumpHeight;
        }
    }

    public void OnCrouch(InputAction.CallbackContext obj)
    {
        if (playerData == null) return;

        IsCrouching = !IsCrouching;
        characterController.height = IsCrouching ? playerData.crouchHeight : normalHeight;
        currentSpeed = IsCrouching ? playerData.crouchSpeed : playerData.walkSpeed;
    }

    public void OnSprint(InputAction.CallbackContext obj)
    {
        if (playerData == null) return;

        IsSprinting = !IsSprinting;

        if (!IsCrouching)
        {
            currentSpeed = IsSprinting ? playerData.sprintSpeed : playerData.walkSpeed;
        }
    }

    private void Update()
    {
        if (playerData == null) return;

        if (characterController.isGrounded && movementVelocity.y < 0)
        {
            movementVelocity.y = -2f;
        }

        movement = transform.right * movementInput.x + transform.forward * movementInput.y;
        movementVelocity.y -= playerData.gravity * Time.deltaTime;

        characterController.Move(((movement * currentSpeed) + movementVelocity) * Time.deltaTime);
    }
}