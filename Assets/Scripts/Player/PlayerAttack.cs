using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerData playerData;

    [Header("Scene References")]
    public Transform CameraTransform;

    private RaycastHit raycastHit;

    private void OnEnable()
    {
        InputManager.Actions.Game.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        InputManager.Actions.Game.Attack.performed -= OnAttack;
    }

    public void OnAttack(InputAction.CallbackContext obj)
    {
        CastRay();
    }

    private void CastRay()
    {
        if (playerData == null) return;

        if (Physics.Raycast(
            CameraTransform.position,
            CameraTransform.forward,
            out raycastHit,
            playerData.weaponDistance,
            playerData.damageableLayers))
        {
            if (raycastHit.collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if (damageable.IsAlive)
                    damageable.ApplyDamage(playerData.weaponDamage);
            }
        }
    }
}