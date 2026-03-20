using UnityEngine;
using TMPro;

public class PerformanceUIManager : MonoBehaviour
{
    [Header("Spawners")]
    public AddressableStressSpawner enemySpawner;
    public AddressableStressSpawner ballSpawner;

    [Header("UI Elements")]
    public TextMeshProUGUI enemyStatsText;
    public TextMeshProUGUI ballStatsText;
    public TextMeshProUGUI fpsText;

    private float deltaTime = 0.0f;

    void Update()
    {
        // Calculate FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";

        // Update Enemy Stats
        if (enemySpawner != null)
        {
            enemyStatsText.text = $"ENEMIES\nStatus: {(enemySpawner.IsSpawning ? "<color=green>SPAWNING</color>" : "<color=red>STOPPED</color>")}\n" +
                                  $"Active: {enemySpawner.ActiveCount}\n" +
                                  $"Pool Size: {enemySpawner.TotalPoolCount}";
        }

        // Update Ball Stats
        if (ballSpawner != null)
        {
            ballStatsText.text = $"BALLS\nStatus: {(ballSpawner.IsSpawning ? "<color=green>SPAWNING</color>" : "<color=red>STOPPED</color>")}\n" +
                                 $"Active: {ballSpawner.ActiveCount}\n" +
                                 $"Pool Size: {ballSpawner.TotalPoolCount}";
        }
    }
}