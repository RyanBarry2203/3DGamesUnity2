using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable
{
    void Initialize(ObjectPool<GameObject> pool);
    void ReturnToPool();
}