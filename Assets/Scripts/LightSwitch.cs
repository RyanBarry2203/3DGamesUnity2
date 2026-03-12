using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private Light[] ConnectedLights;
    [SerializeField] private bool IsOn = true;

    private void Awake()
    {
        UpdateConnectedLights();
    }
    public bool CanInteractWith(GameObject interactor)
    {
        return true;
    }
    public void Interact(GameObject interactor)
    {
        IsOn = !IsOn;
    }
    private void UpdateConnectedLights()
    {
        foreach (var light in ConnectedLights)
        {
            light.enabled = IsOn;
        }
    }
}
