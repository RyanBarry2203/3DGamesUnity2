using UnityEngine;
using UnityEngine.InputSystem;

public class StressTestSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject prefabToSpawn;
    public int spawnCountPerFrame = 10;
    public float spawnRadius = 20f;

    [Header("State")]
    public bool isSpawning = false;
    private int totalSpawned = 0;

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
                Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;

                GameObject newObj = Instantiate(prefabToSpawn, randomPos, Random.rotation);

                totalSpawned++;
            }
        }
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.skin.label.fontSize = 24;
        GUI.Label(new Rect(10, 10, 400, 50), $"Total Objects: {totalSpawned}");
    }
}