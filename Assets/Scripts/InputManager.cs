using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static ApplicationActions Actions { get; private set; }

    void Awake()
    {
        Actions = new ApplicationActions();
        Actions.Enable();
    }
}