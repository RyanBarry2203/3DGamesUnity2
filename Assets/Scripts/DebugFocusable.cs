using UnityEngine;

public class DebugFocusable : MonoBehaviour, IFocusable
{
    public Color highlightColor = Color.white;
    private Color origionalColor;
    private MeshRenderer meshRenderer;
    private Material material;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            material = meshRenderer.material;
        }
    }
    public void Focus(GameObject interactor)
    {
        if (material != null)
        {
            origionalColor = material.GetColor("_EmissionColor");
            material.SetColor("EmissionColor", highlightColor);
            material.EnableKeyword("_EMISSION");
        }
    }
    public void Unfocus(GameObject interactor)
    {
        if (material != null)
        {
            material.SetColor("EmissionColor", origionalColor);
            material.DisableKeyword("_EMISSION");
        }
    }
}
