using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData playerData;

    [Header("Scene References")]
    public Transform CameraTransform;

    private RaycastHit raycastHit;
    private IInteractable currentInteractable;
    private IFocusable currentFocusable;

    private void OnEnable()
    {
        InputManager.Actions.Game.Interact.performed += OnInteract;
    }
    private void OnDisable()
    {
        InputManager.Actions.Game.Interact.performed -= OnInteract;
    }

    private void ClearFocus()
    {
        if (currentFocusable != null && (currentFocusable as MonoBehaviour) != null)
        {
            currentFocusable.Unfocus(gameObject);
        }

        currentFocusable = null;
        currentInteractable = null;
    }

    private void CastRay()
    {
        if (playerData == null) return;

        if (Physics.Raycast(
            CameraTransform.position,
            CameraTransform.forward,
            out raycastHit,
            playerData.interactionDistance,
            playerData.interactionLayers))
        {
            raycastHit.collider.gameObject.TryGetComponent<IInteractable>(out currentInteractable);
            raycastHit.collider.gameObject.TryGetComponent<IFocusable>(out IFocusable newFocusable);

            if (newFocusable != currentFocusable)
            {
                ClearFocus();
                currentFocusable = newFocusable;

                if (currentFocusable != null)
                    currentFocusable.Focus(gameObject);
            }
        }
        else
        {
            ClearFocus();
        }
    }

    void Update()
    {
        CastRay();
    }

    public void OnInteract(InputAction.CallbackContext obj)
    {
        if (currentInteractable != null)
        {
            if (currentInteractable.CanInteractWith(gameObject))
            {
                currentInteractable.Interact(gameObject);
            }
        }
    }
}