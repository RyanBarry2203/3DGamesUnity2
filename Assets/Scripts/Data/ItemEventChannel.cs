using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Item Event Channel")]
public class ItemEventChannelSO : ScriptableObject
{
    public UnityAction<ItemData> OnEventRaised;

    public void RaiseEvent(ItemData item)
    {
        OnEventRaised?.Invoke(item);
    }
}