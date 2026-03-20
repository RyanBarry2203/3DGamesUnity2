using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnerStats", menuName = "Game Data/Spawner Stats")]
public class SpawnerStatsSO : ScriptableObject
{
    [Header("Read-Only Runtime Data")]
    public string spawnerName = "Spawner";
    public bool isSpawning = false;
    public bool isDespawning = false;
    public int activeCount = 0;
    public int totalPoolCount = 0;

    private void OnDisable()
    {
        isSpawning = false;
        activeCount = 0;
        totalPoolCount = 0;
    }
}