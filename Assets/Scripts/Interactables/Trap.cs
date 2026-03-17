using UnityEngine;
using UnityEngine.Pool;

public class Trap : MonoBehaviour, IInteractable, IFocusable, IDamageable, IPoolable
{
    public TrapData trapData;
    public float health;

    private Renderer[] renderers;
    private MaterialPropertyBlock propBlock;
    private ObjectPool<GameObject> myPool;
    private bool isDead = false;

    private Animator animator;

    public bool IsAlive => health > 0 && !isDead;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        propBlock = new MaterialPropertyBlock();

        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        if (trapData != null)
        {
            health = trapData.startingHealth;
            SetColor(trapData.origionalColor);
        }
        isDead = false;
    }

    public void Initialize(ObjectPool<GameObject> pool)
    {
        myPool = pool;
    }

    public void ReturnToPool()
    {
        if (myPool != null)
            myPool.Release(gameObject);
        else
            gameObject.SetActive(false);
    }

    private void SetColor(Color color)
    {
        if (renderers == null || renderers.Length == 0) return;

        foreach (Renderer r in renderers)
        {
            r.GetPropertyBlock(propBlock);
            propBlock.SetColor("_BaseColor", color);
            r.SetPropertyBlock(propBlock);
        }
    }

    public void Focus(GameObject interactor) => SetColor(trapData.focus);
    public void Unfocus(GameObject interactor) => SetColor(trapData.origionalColor);

    public bool CanInteractWith(GameObject interactor)
    {
        return interactor.TryGetComponent<BasicInventory>(out var inventory) && inventory.HasItem(trapData.disarmKit);
    }

    public void Interact(GameObject interactor)
    {
        SetColor(trapData.disarmed);
        Debug.Log("You disarmed the trap!");
        isDead = true;
    }

    public void ApplyDamage(float damageAmount)
    {
        if (!IsAlive) return;

        health -= damageAmount;

        if (health <= 0)
        {
            isDead = true;
            Debug.Log("Trap Destroyed");
            ReturnToPool();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            Debug.Log("Trap snapped on " + other.name + "!");

            // Deal damage to the player
            damageable.ApplyDamage(trapData.damageToPlayer);

            // Play the animation
            if (animator != null)
            {
                animator.SetTrigger("Snap");
            }

            isDead = true;
        }
    }
}