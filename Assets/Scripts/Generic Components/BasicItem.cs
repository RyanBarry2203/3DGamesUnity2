using UnityEngine;

public class BasicItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData item;

    public bool CanInteractWith(GameObject interactor)
    {
        return true;
    }
    public void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent<BasicInventory>(out BasicInventory inventory))
        {
            inventory.AddItem(item);
            Debug.Log($"Picked up {item.itemName}.");
            Destroy(gameObject);
        }
    }
}
