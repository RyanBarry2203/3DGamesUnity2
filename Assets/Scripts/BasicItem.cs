using UnityEngine;

public class BasicItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemName;

    public bool CanInteractWith(GameObject interactor)
    {
        return true;
    }
    public void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent<BasicInventory>(out BasicInventory invenmtory))
        {
            invenmtory.AddItem(itemName);
            Debug.Log($"Picked up {itemName}.");
            Destroy(gameObject);
        }
    }
}
