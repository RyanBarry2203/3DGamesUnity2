using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game Data/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;

}