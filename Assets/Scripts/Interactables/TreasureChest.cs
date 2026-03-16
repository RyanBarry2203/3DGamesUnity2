using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable, IFocusable
{
    private Color origionalColor;

    public ChestData chestData;
    //[SerializeField] public ItemData itemData;


    private bool isOpened = false;

    private Material material;

    private void Awake()
    {
        if (TryGetComponent<Renderer>(out Renderer renderer))
        {
            material = renderer.material;
            origionalColor = material.GetColor("_EmissionColor");
        }
    }
    private void Start()
    {

    }
    public void Focus(GameObject interactor)
    {
        if (material != null)
        {
            material.EnableKeyword("_EMISSION");

            if (!isOpened)
            {
                material.SetColor("_EmissionColor", chestData.lockedColor);
            }
            else
            {
                material.SetColor("_EmissionColor", chestData.unlockedColor);
            }
        }
    }
    public void Unfocus(GameObject interactor)
    {
        if (material != null)
        {
            material.SetColor("_EmissionColor", origionalColor);
            material.DisableKeyword("_EMISSION");
        }
    }
    public bool CanInteractWith(GameObject interacvtor)
    {
        return !isOpened;
    }
    public void Interact(GameObject interactor)
    {
        isOpened = true;
        Debug.Log("You opened the chest and found a treasure!");

        if (material != null)
        {
            material.SetColor("_EmissionColor", chestData.unlockedColor);
        }

        if (interactor.TryGetComponent<BasicInventory>(out BasicInventory inventory))
        {
            inventory.AddItem(chestData.itemInside);
            Debug.Log($"Picked up {chestData.itemInside.itemName}.");
            //Destroy(gameObject);
        }
    }

}
