using UnityEngine;

public class Gold : MonoBehaviour, IInteractable, IFocusable
{
    [SerializeField] private string itemName;

    private bool chestIsOpened = false;


    private void Start()
    {
        if (TryGetComponent<TrasureChest>(out bool isOpened))
        {
            
        }
    }
    public bool CanInteractWith(GameObject interactor)
    {
        if (chestIsOpened == true)
        {
            return true;
        }
        else
        {
            return false;
        }
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
