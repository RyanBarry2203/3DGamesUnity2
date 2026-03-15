using UnityEngine;

[CreateAssetMenu(fileName = "NewChestData", menuName = "Game Data/Chest Data")]
public class ChestData : ScriptableObject
{
    [Header("Visuals")]
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.green;

    [Header("Rewards")]
    public ItemData itemInside;
}