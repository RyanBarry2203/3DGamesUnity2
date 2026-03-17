using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemEventChannelSO itemAddedChannel;

    private void OnEnable() => itemAddedChannel.OnEventRaised += UpdateUI;
    private void OnDisable() => itemAddedChannel.OnEventRaised -= UpdateUI;

    private void UpdateUI(ItemData item)
    {
        Debug.Log($"UI Updated: Displaying {item.itemName} icon.");
    }
}
