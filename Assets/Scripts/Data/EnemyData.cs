using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Health & Visuals")]
    public float maxHealth = 100f;
    public Color healthyColor = Color.white;
    public Color woundedColor = Color.yellow;
    public Color criticalColor = Color.red;

    [Header("AI Settings")]
    public float chaseSpeed = 3.5f;
    public float detectionRadius = 20f;
    public LayerMask playerLayer;
}