using UnityEngine;

public class ChestEmission : MonoBehaviour, IFocusable
{
    public Color highlightColor = Color.blue;
    private Color originalColor;
    private Material material;

    private void Awake()
    {
        if (TryGetComponent<Renderer>(out Renderer renderer))
        {
            material = renderer.material;
            originalColor = material.GetColor("_BaseColor");
        }
    }

    public void Focus(GameObject interactor)
    {
        if (material != null)
        {
            material.SetColor("_BaseColor", highlightColor);
        }
    }

    public void Unfocus(GameObject interactor)
    {
        if (material != null)
        {
            material.SetColor("_BaseColor", originalColor);
        }
    }
}
