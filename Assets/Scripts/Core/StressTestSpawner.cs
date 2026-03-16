using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using System.Collections.Generic;

public class StressTestSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject prefabToSpawn;
    public int spawnCountPerFrame = 10;
    public float spawnRadius = 20f;

    [Tooltip("Maximum number of objects alive at once before we start recycling them.")]
    public int maxActiveObjects = 5000;

    [Header("State")]
    public bool isSpawning = false;

    private ObjectPool<GameObject> objectPool;
    private Queue<GameObject> activeObjects = new Queue<GameObject>();

    private void Awake()
    {
        objectPool = new ObjectPool<GameObject>(
            createFunc: CreatePooledItem,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity: 1000,
            maxSize: 10000
        );
    }

    private GameObject CreatePooledItem()
    {
        return Instantiate(prefabToSpawn);
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            isSpawning = !isSpawning;
            Debug.Log(isSpawning ? "Spawning Started!" : "Spawning Stopped.");
        }

        if (isSpawning)
        {
            for (int i = 0; i < spawnCountPerFrame; i++)
            {
                GameObject newObj = objectPool.Get();

                newObj.transform.position = transform.position + Random.insideUnitSphere * spawnRadius;
                newObj.transform.rotation = Random.rotation;

                activeObjects.Enqueue(newObj);

                if (activeObjects.Count > maxActiveObjects)
                {
                    GameObject oldestObj = activeObjects.Dequeue();
                    objectPool.Release(oldestObj);
                }
            }
        }
    }

    private void OnGUI()
    {
        GUI.color = Color.green;
        GUI.skin.label.fontSize = 24;
        GUI.Label(new Rect(10, 10, 400, 50), $"Active Objects: {activeObjects.Count}");
        GUI.Label(new Rect(10, 40, 400, 50), $"Total in Pool Memory: {objectPool.CountAll}");
    }
}