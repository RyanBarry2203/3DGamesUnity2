using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float WeaponDistance = 3;
    public float WeaponDamage = 25;

    public Transform CameraTransform;
    public LayerMask DamageableLayers;
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
        if (Physics.Raycast(
            CameraTransform.position,
            CameraTransform.forward,
            out raycastHit,
            WeaponDistance,
            DamageableLayers))
        {
            if (raycastHit.collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if (damageable.IsAlive)
                    damageable.ApplyDamage(WeaponDamage);
            }
        }
    }
}