using UnityEngine;
using TMPro;

public class PerformanceUIManager : MonoBehaviour
{
    [Header("Data Sources (Scriptable Objects)")]
    public SpawnerStatsSO enemyStatsData;
    public SpawnerStatsSO ballStatsData;

    [Header("UI Elements")]
    public TextMeshProUGUI enemyStatsText;
    public TextMeshProUGUI ballStatsText;
    public TextMeshProUGUI fpsText;

    [Header("Optimization")]
    public float uiUpdateInterval = 0.2f;

    private float timer = 0f;
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        timer += Time.unscaledDeltaTime;
        if (timer >= uiUpdateInterval)
        {
            UpdateUI();
            timer = 0f;
        }
    }

    private void UpdateUI()
    {
        float fps = 1.0f / deltaTime;
        if (fpsText != null) fpsText.text = $"FPS: {Mathf.Ceil(fps)}";

        // Read directly from the Scriptable Objects!
        if (enemyStatsData != null && enemyStatsText != null)
        {
            enemyStatsText.text = $"{enemyStatsData.spawnerName}\n" +
                                  $"Status: {(enemyStatsData.isSpawning ? "<color=green>SPAWNING</color>" : "<color=red>STOPPED</color>")}\n" +
                                  $"Active: {enemyStatsData.activeCount}\n" +
                                  $"Pool Size: {enemyStatsData.totalPoolCount}";
        }

        if (ballStatsData != null && ballStatsText != null)
        {
            ballStatsText.text = $"{ballStatsData.spawnerName}\n" +
                                 $"Status: {(ballStatsData.isSpawning ? "<color=green>SPAWNING</color>" : "<color=red>STOPPED</color>")}\n" +
                                 $"Active: {ballStatsData.activeCount}\n" +
                                 $"Pool Size: {ballStatsData.totalPoolCount}";
        }
    }
}