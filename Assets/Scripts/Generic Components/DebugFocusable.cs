//using UnityEngine;

//public class DebugFocusable : MonoBehaviour, IFocusable
//{
//    [ColorUsage(true, true)] public Color highlightColor = Color.white;
//    private Color origionalColor;
//    private MeshRenderer meshRenderer;
//    private Material material;

//    private void Awake()
//    {
//        meshRenderer = GetComponent<MeshRenderer>();

//        if (meshRenderer != null)
//        {
//            material = meshRenderer.material;
//        }
//    }
//    public void Focus(GameObject interactor)
//    {
//        Debug.Log($"Focused on {gameObject.name} by {interactor.name}");

//        if (material != null)
//        {
//            origionalColor = material.GetColor("_BaseColor");
//            material.SetColor("_BaseColor", highlightColor);
//            material.EnableKeyword("_EMISSION");
//        }
//    }
//    public void Unfocus(GameObject interactor)
//    {
//        if (material != null)
//        {
//            material.SetColor("_BaseColor", origionalColor);
//            material.DisableKeyword("_EMISSION");
//        }
//    }
//}


using UnityEngine;

public class DebugFocusable : MonoBehaviour, IFocusable
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
