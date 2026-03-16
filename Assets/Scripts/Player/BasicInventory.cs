using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicInventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> items = new();


    public event Action<ItemData> OnItemAdded;

    public void AddItem(ItemData item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log($"Added {item.itemName} to inventory.");


            OnItemAdded?.Invoke(item);
        }
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }
}