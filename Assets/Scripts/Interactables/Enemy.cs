using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Data")]
    public EnemyData data;

    private float currentHealth;
    private bool isDead = false;

    private Renderer[] renderers;
    private MaterialPropertyBlock propBlock;

    public bool IsAlive => currentHealth > 0 && !isDead;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        propBlock = new MaterialPropertyBlock();
    }

    private void OnEnable()
    {
        if (data != null)
        {
            currentHealth = data.maxHealth;
            UpdateColor(data.healthyColor);
        }
        isDead = false;
    }

    public void ApplyDamage(float damageAmount)
    {
        if (!IsAlive) return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Enemy Killed");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"Damage applied: {damageAmount}. Remaining health: {currentHealth}");

            float healthPercent = currentHealth / data.maxHealth;

            if (healthPercent <= 0.33f)
                UpdateColor(data.criticalColor);
            else if (healthPercent <= 0.66f)
                UpdateColor(data.woundedColor);
        }
    }

    private void UpdateColor(Color color)
    {
        if (renderers == null || renderers.Length == 0) return;

        foreach (Renderer r in renderers)
        {
            r.GetPropertyBlock(propBlock);

            propBlock.SetColor("_BaseColor", color);
            propBlock.SetColor("_EmissionColor", color);

            r.SetPropertyBlock(propBlock);
        }
    }
}