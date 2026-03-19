using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.SetAnimationSpeed(0f);
    }

    public void UpdateState(Enemy enemy)
    {

        Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, enemy.data.detectionRadius, enemy.data.playerLayer);

        if (hitColliders.Length > 0)
        {
            enemy.SetTarget(hitColliders[0].transform);
            enemy.ChangeState(new EnemyChaseState());
        }
    }

    public void ExitState(Enemy enemy) { }
}