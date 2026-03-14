using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(float damageAmount);
    bool IsAlive { get; }
}
