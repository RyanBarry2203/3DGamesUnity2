using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private float health = 100;

    public Color wounded = Color.yellow;
    public Color critical = Color.red;
    private Color origionalColor;

    private Material material;

    private bool isDead = false;

    private void Awake()
    {
        if (TryGetComponent<Renderer>(out Renderer renderer))
        {
            material = renderer.material;
            origionalColor = material.GetColor("_EmissionColor");
        }
    }
    public bool IsAlive
    {
        get { return health > 0 && !isDead; }
    }
    public void ApplyDamage(float damageAmount)
    {

        if (!IsAlive) return;

        health -= damageAmount;

        if (health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
            Debug.Log("Enemy Killed");
            gameObject.SetActive(false);
        }
        else
        {
            if (material != null)
            {
                material.EnableKeyword("_EMISSION");
                if (health <= 66 && health > 33)
                {
                    material.SetColor("_EmissionColor", wounded);
                }
                else if (health <= 32)
                {
                    material.SetColor("_EmissionColor", critical);
                }
            }
            Debug.Log($"Damage applied: {damageAmount}. Remaining health: {health}");
        }
    }

}
