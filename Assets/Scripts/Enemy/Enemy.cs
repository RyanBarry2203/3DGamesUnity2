using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour, IDamageable, IPoolable
{
    [Header("Data")]
    public EnemyData data;

    private float currentHealth;
    private bool isDead = false;

    private Renderer[] renderers;
    private MaterialPropertyBlock propBlock;
    private Animator animator;
    private ObjectPool<GameObject> myPool;

    private IEnemyState currentState;
    public Transform Target { get; private set; }
    public Rigidbody Rb { get; private set; }

    public bool IsAlive => currentHealth > 0 && !isDead;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        propBlock = new MaterialPropertyBlock();
        animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (data != null)
        {
            currentHealth = data.maxHealth;
            UpdateColor(data.healthyColor);
        }
        isDead = false;

        ChangeState(new EnemyIdleState());
    }

    private void Update()
    {
        if (!IsAlive) return;

        currentState?.UpdateState(this);
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

    public void ChangeState(IEnemyState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState?.EnterState(this);
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    public void SetAnimationSpeed(float speed)
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }

    public void ApplyDamage(float damageAmount)
    {
        if (!IsAlive) return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            isDead = true;
            ReturnToPool();
        }
        else
        {
            float healthPercent = currentHealth / data.maxHealth;
            if (healthPercent <= 0.33f) UpdateColor(data.criticalColor);
            else if (healthPercent <= 0.66f) UpdateColor(data.woundedColor);
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