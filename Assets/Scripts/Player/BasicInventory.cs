//using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BasicInventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> items = new();

    public void AddItem(ItemData item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log($"Added {item} to inventory.");
        }
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }
}
