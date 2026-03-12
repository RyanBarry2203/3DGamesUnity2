using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static ApplicationActions Actions;

    private void Awake()
    {
        if (Actions == null)
        {
            Actions = new ApplicationActions();
        }

        Actions.Enable();
    }

    private void OnDisable()
    {
        if (Actions != null)
        {
            Actions.Disable();
        }
    }
}