using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Pool;

public class AsyncPoolManager : MonoBehaviour
{
    [Header("Addressable Settings")]
    [SerializeField] private AssetReference prefabReference;

    [Header("Pool Settings")]
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private ObjectPool<GameObject> pool;
    private GameObject loadedPrefab;
    private bool isInitialized = false;

    public async Task InitializePoolAsync()
    {
        if (isInitialized) return;

        Debug.Log("Starting Async Load for Pool...");

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(prefabReference);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            loadedPrefab = handle.Result;

            pool = new ObjectPool<GameObject>(
                createFunc: CreatePooledItem,
                actionOnGet: OnTakeFromPool,
                actionOnRelease: OnReturnedToPool,
                actionOnDestroy: OnDestroyPoolObject,
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            );

            isInitialized = true;
            Debug.Log("Pool Initialized and Ready!");
        }
        else
        {
            Debug.LogError("Failed to load Addressable prefab.");
        }
    }

    private GameObject CreatePooledItem()
    {
        GameObject instance = Instantiate(loadedPrefab);

        if (instance.TryGetComponent<IPoolable>(out var poolable))
        {
            poolable.Initialize(pool);
        }

        return instance;
    }

    private void OnTakeFromPool(GameObject obj) => obj.SetActive(true);
    private void OnReturnedToPool(GameObject obj) => obj.SetActive(false);
    private void OnDestroyPoolObject(GameObject obj) => Destroy(obj);

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Trying to spawn before the pool is initialized!");
            return null;
        }

        GameObject obj = pool.Get();
        obj.transform.SetPositionAndRotation(position, rotation);
        return obj;
    }

    private void OnDestroy()
    {
        if (loadedPrefab != null)
        {
            Addressables.Release(loadedPrefab);
        }
    }
}