using UnityEngine;

public class DataDrivenMonolithicObject : MonoBehaviour, IInteractable, IFocusable, IDamageable
{
    [Header("Data Definition")]
    public MonolithData data;

    [Header("Current State")]
    private float currentHealth;
    private bool isDestroyed = false;

    private Material material;
    private Color originalColor;

    public bool IsAlive
    {
        get { return currentHealth > 0 && !isDestroyed; }
    }

    private void Awake()
    {
        if (TryGetComponent<Renderer>(out Renderer renderer))
        {
            material = renderer.material;
            originalColor = material.GetColor("_EmissionColor");
        }
    }

    private void OnEnable()
    {

        if (data != null)
        {
            currentHealth = data.maxHealth;
            isDestroyed = false;
        }
    }
    public void Focus(GameObject interactor)
    {
        if (material != null && data != null)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", data.glowColor);
        }
    }

    public void Unfocus(GameObject interactor)
    {
        if (material != null)
        {
            material.DisableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", originalColor);
        }
    }

    public bool CanInteractWith(GameObject interactor)
    {
        if (interactor.TryGetComponent<BasicInventory>(out BasicInventory inventory) && data != null)
        {
            return inventory.HasItem(data.requredItem);
        }
        return false;
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("Object activated!");
        gameObject.SetActive(false);
    }
    public void ApplyDamage(float damageAmount)
    {
        if (!IsAlive) return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            isDestroyed = true;
            Debug.Log("Monolith Destroyed");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Damage applied: {damageAmount}. Remaining health: {currentHealth}");
        }
    }
}