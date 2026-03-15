using UnityEngine;

public class DebugDamageable : MonoBehaviour, IDamageable
{
   private float health = 100;

    public bool IsAlive
    {
        get { return health > 0; }
    }

    public void ApplyDamage(float damageAmount)
    {
        health -= damageAmount;

        if (!IsAlive)
        {
            Destroy(gameObject);
        }

        Debug.Log($"Damage applied: {damageAmount}. Remaining health: {health}");
    }
}
