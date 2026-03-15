using UnityEngine;

[CreateAssetMenu(fileName = "NewLightSwitchData", menuName = "Game Data/LightSwitchData Data")]
public class LightSwitchData : ScriptableObject
{
    [Header("Visuals")]
    public Color focus = Color.blue;

    [Header("Required Item")]
    public ItemData requiredItem;
}
