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
    private bool isSpawning = false;

    private Queue<GameObject> activeObjects = new Queue<GameObject>();

    private async void Start()
    {
        if (spawnerData == null)
        {
            Debug.LogError($"No SpawnerData assigned to {gameObject.name}!");
            return;
        }

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
        isSpawning = !isSpawning;
        Debug.Log($"{gameObject.name} Spawning is now {(isSpawning ? "ON" : "OFF")}");
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
            Debug.Log($"Pool Initialized for {loadedPrefab.name}!");
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

    private void Update()
    {
        if (!isInitialized) return;

        if (isSpawning)
        {
            SpawnBatch();
        }
    }

    private void SpawnBatch()
    {
        for (int i = 0; i < spawnerData.spawnCountPerFrame; i++)
        {
            GameObject newObj = pool.Get();

            newObj.transform.position = transform.position + Random.insideUnitSphere * spawnerData.spawnRadius;
            newObj.transform.rotation = Random.rotation;

            if (newObj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            activeObjects.Enqueue(newObj);

            if (activeObjects.Count > spawnerData.maxActiveObjects)
            {
                GameObject oldestObj = activeObjects.Dequeue();

                if (oldestObj.activeInHierarchy)
                {
                    pool.Release(oldestObj);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (loadedPrefab != null)
        {
            Addressables.Release(loadedPrefab);
        }
    }
}