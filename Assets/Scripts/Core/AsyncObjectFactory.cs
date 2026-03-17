using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;

public class AsyncObjectFactory : MonoBehaviour
{
    [SerializeField] private AssetReference objectToSpawn;


    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: CreateSetup,
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Addressables.ReleaseInstance(obj),
            defaultCapacity: 20,
            maxSize: 100
        );
    }

    public async Task<GameObject> SpawnObjectAsync(Vector3 position)
    {
        GameObject obj = pool.Get();
        obj.transform.position = position;
        return obj;
    }


    private GameObject CreateSetup()
    {
        var op = Addressables.InstantiateAsync(objectToSpawn);
        GameObject instance = op.WaitForCompletion();

        if (instance.TryGetComponent<IPoolable>(out var poolable))
        {
            poolable.Initialize(pool);
        }

        return instance;
    }
}

public interface IPoolable
{
    void Initialize(ObjectPool<GameObject> pool);
    void ReturnToPool();
}