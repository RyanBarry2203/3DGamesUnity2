using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.SetAnimationSpeed(1f);
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Target == null || !enemy.Target.gameObject.activeInHierarchy)
        {
            enemy.ChangeState(new EnemyIdleState());
            return;
        }

        Vector3 direction = (enemy.Target.position - enemy.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        enemy.transform.position += direction * enemy.data.chaseSpeed * Time.deltaTime;

        if (Vector3.Distance(enemy.transform.position, enemy.Target.position) > enemy.data.detectionRadius * 1.5f)
        {
            enemy.SetTarget(null);
            enemy.ChangeState(new EnemyIdleState());
        }
    }

    public void ExitState(Enemy enemy) { }
}