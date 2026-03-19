using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 100f;
    public Color healthyColor = Color.white;
    public Color woundedColor = Color.yellow;
    public Color criticalColor = Color.red;
}