using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Transform CameraTransform;
    public float InteractionDistance = 3;
    public LayerMask InteractionLayers;
    private RaycastHit raycastHit;

    private IInteractable currentInteractable;

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
        currentInteractable = null;
    }
    private void CastRay()
    {
        if (Physics.Raycast(
            CameraTransform.position,
            CameraTransform.forward,
            out raycastHit,
            InteractionDistance,
            InteractionLayers))
        {
            raycastHit.collider.gameObject.TryGetComponent<IInteractable>(out currentInteractable);  
        }
        else
        {
            ClearFocus();
        }
    }

    // Update is called once per frame
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
