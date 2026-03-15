using UnityEngine;

public class Trap : MonoBehaviour, IInteractable, IFocusable, IDamageable
{
    public TrapData trapData;

    private Material material;
    private bool isDead = false;
    public float health;

    public bool IsAlive
    {
        get { return health > 0 && !isDead; }
    }

    private void Awake()
    {
        if (TryGetComponent<Renderer>(out Renderer renderer))
        {
            material = renderer.material;
            trapData.origionalColor = material.GetColor("_EmissionColor");
        }
        health = trapData.startingHealth;
    }

    public void Focus(GameObject interactor)
    {
        if (material != null)
        {
            material.SetColor("_BaseColor", trapData.focus);
        }
    }

    public void Unfocus(GameObject interactor)
    {
        if (material != null)
        {
            material.SetColor("_BaseColor", trapData.origionalColor);
        }
    }
    public bool CanInteractWith(GameObject interactor)
    {
        if (interactor.TryGetComponent<BasicInventory>(out BasicInventory inventory))
        {
            return inventory.HasItem(trapData.disarmKit);
        }

        return false;
    }
    public void Interact(GameObject interactor)
    {
        if (material != null)
        {
            material.SetColor("_BaseColor", trapData.disarmed);
            Debug.Log("You disarmed the trap!");
            gameObject.SetActive(false);
        }
    }
    public void ApplyDamage(float damageAmount)
    {

        if (!IsAlive) return;

        health -= damageAmount;

        if (health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
            Debug.Log("Trap Destroyed");
            gameObject.SetActive(false);
        }
        else
        {

            Debug.Log($"Damage applied: {damageAmount}. Remaining health: {health}");
        }
    }
}
