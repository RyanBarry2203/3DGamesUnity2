using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AsyncPoolManager))]
public class PoolTester : MonoBehaviour
{
    private AsyncPoolManager poolManager;

    void Awake()
    {
        poolManager = GetComponent<AsyncPoolManager>();
    }

    async void Start()
    {
        await poolManager.InitializePoolAsync();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
        {
            poolManager.SpawnObject(transform.position, Quaternion.identity);
        }
    }
}