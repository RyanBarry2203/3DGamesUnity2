using UnityEngine;

[CreateAssetMenu(fileName = "NewMonolithData", menuName = "Game Data/Monolith Data")]
public class MonolithData : ScriptableObject
{
    [Header("Base Stats")]
    public float maxHealth = 75f;
    public Color glowColor = Color.cyan;
    public ItemData requredItem;
}