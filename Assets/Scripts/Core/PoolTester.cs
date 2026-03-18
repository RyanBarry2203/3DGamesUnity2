using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AsyncPoolManager))]
public class PoolTester : MonoBehaviour
{
     [SerializeField]private AsyncPoolManager poolManager;

    void Awake()
    {
        poolManager = GetComponent<AsyncPoolManager>();
    }

    async void Start()
    {
        await poolManager.InitializePoolAsync();
        poolManager.SpawnObject(transform.position, Quaternion.identity);
    }
        
    
}