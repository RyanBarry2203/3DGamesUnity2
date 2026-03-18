using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicInventory : MonoBehaviour
{
    private HashSet<ItemData> items = new HashSet<ItemData>();

    [SerializeField] private ItemEventChannelSO itemAddedChannel;
    public event Action<ItemData> OnItemAdded;

    public void AddItem(ItemData item)
    {
        if (items.Add(item))
        {
            if (itemAddedChannel != null)
                itemAddedChannel.RaiseEvent(item);
        }
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }
}