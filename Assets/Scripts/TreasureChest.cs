using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable, IFocusable
{
    public Color lockedColor = Color.red;
    public Color unlockledColor = Color.green;
    private Color origionalColor;

    [SerializeField] private string itemName;

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
                material.SetColor("_EmissionColor", lockedColor);
            }
            else
            {
                material.SetColor("_EmissionColor", unlockledColor);
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
            material.SetColor("_EmissionColor", unlockledColor);
        }

        if (interactor.TryGetComponent<BasicInventory>(out BasicInventory invenmtory))
        {
            invenmtory.AddItem(itemName);
            Debug.Log($"Picked up {itemName}.");
            //Destroy(gameObject);
        }
    }

}
