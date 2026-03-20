using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PerformanceUIManager : MonoBehaviour
{
    [Header("Data Sources (Scriptable Objects)")]
    public SpawnerStatsSO enemyStatsData;
    public SpawnerStatsSO ballStatsData;

    [Header("UI Elements")]
    public GameObject statsPanel;
    public TextMeshProUGUI enemyStatsText;
    public TextMeshProUGUI ballStatsText;
    public TextMeshProUGUI fpsText;

    [Header("Input & Optimization")]
    public InputActionReference toggleUIAction;
    public float uiUpdateInterval = 0.2f;

    private float timer = 0f;
    private float deltaTime = 0.0f;

    private void OnEnable()
    {
        if (toggleUIAction != null && toggleUIAction.action != null)
        {
            toggleUIAction.action.Enable();
            toggleUIAction.action.performed += OnToggleUI;
        }
    }

    private void OnDisable()
    {
        if (toggleUIAction != null && toggleUIAction.action != null)
        {
            toggleUIAction.action.performed -= OnToggleUI;
        }
    }

    private void OnToggleUI(InputAction.CallbackContext context)
    {
        if (statsPanel != null) statsPanel.SetActive(!statsPanel.activeSelf);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (statsPanel != null && !statsPanel.activeSelf) return;

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

        if (enemyStatsData != null && enemyStatsText != null)
        {
            enemyStatsText.text = $"{enemyStatsData.spawnerName}\n" +
                                  $"Status: {GetStatusString(enemyStatsData)}\n" +
                                  $"Active: {enemyStatsData.activeCount}\n" +
                                  $"Pool Size: {enemyStatsData.totalPoolCount}";
        }

        if (ballStatsData != null && ballStatsText != null)
        {
            ballStatsText.text = $"{ballStatsData.spawnerName}\n" +
                                 $"Status: {GetStatusString(ballStatsData)}\n" +
                                 $"Active: {ballStatsData.activeCount}\n" +
                                 $"Pool Size: {ballStatsData.totalPoolCount}";
        }
    }

    private string GetStatusString(SpawnerStatsSO stats)
    {
        if (stats.isSpawning && stats.isDespawning) return "<color=yellow>SPAWN & DESPAWN</color>";
        if (stats.isSpawning) return "<color=green>SPAWNING</color>";
        if (stats.isDespawning) return "<color=#FFA500>DESPAWNING</color>";
        return "<color=red>STOPPED</color>";
    }
}