using UnityEngine;


[CreateAssetMenu(fileName = "NewTrapData", menuName = "Game Data/Trap Data")]
public class TrapData : ScriptableObject
{
    [Header("Visuals")]
    public Color focus = Color.red;
    public Color disarmed = Color.green;
    public Color origionalColor;
    public float startingHealth = 50;

    [Header("Disarm Requirements")]
    public ItemData disarmKit;
}
