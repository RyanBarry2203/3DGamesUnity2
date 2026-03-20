using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Pool;
using UnityEngine.InputSystem;

public class AddressableStressSpawner : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private SpawnerData spawnerData;

    private ObjectPool<GameObject> pool;
    private GameObject loadedPrefab;
    private bool isInitialized = false;

    // Expose these for the UI
    public bool IsSpawning { get; private set; } = false;
    public int ActiveCount => activeObjects.Count;
    public int TotalPoolCount => pool != null ? pool.CountAll : 0;

    private Queue<GameObject> activeObjects = new Queue<GameObject>();

    private async void Start()
    {
        if (spawnerData == null) return;
        await InitializePoolAsync();
    }

    private void OnEnable()
    {
        if (spawnerData != null && spawnerData.toggleAction != null)
        {
            spawnerData.toggleAction.action.Enable();
            spawnerData.toggleAction.action.performed += OnToggleSpawning;
        }
    }

    private void OnDisable()
    {
        if (spawnerData != null && spawnerData.toggleAction != null)
        {
            spawnerData.toggleAction.action.performed -= OnToggleSpawning;
        }
    }

    private void OnToggleSpawning(InputAction.CallbackContext context)
    {
        ToggleSpawning();
    }

    public void ToggleSpawning()
    {
        IsSpawning = !IsSpawning;
    }

    private async Task InitializePoolAsync()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(spawnerData.prefabReference);
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
                defaultCapacity: 100,
                maxSize: spawnerData.maxActiveObjects + 100
            );
            isInitialized = true;
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

    private void Update()
    {
        if (!isInitialized) return;
        if (IsSpawning) SpawnBatch();
    }

    private void SpawnBatch()
    {
        for (int i = 0; i < spawnerData.spawnCountPerFrame; i++)
        {
            GameObject newObj = pool.Get();
            Vector2 randomCircle = Random.insideUnitCircle * spawnerData.spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, 2f, randomCircle.y);
            newObj.transform.position = spawnPos;

            if (newObj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            activeObjects.Enqueue(newObj);

            if (activeObjects.Count > spawnerData.maxActiveObjects)
            {
                GameObject oldestObj = activeObjects.Dequeue();
                if (oldestObj.activeInHierarchy) pool.Release(oldestObj);
            }
        }
    }

    private void OnDestroy()
    {
        if (loadedPrefab != null) Addressables.Release(loadedPrefab);
    }
}