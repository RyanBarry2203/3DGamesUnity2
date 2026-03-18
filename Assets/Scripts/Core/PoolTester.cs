using UnityEngine;

public class PoolTester : MonoBehaviour
{
    public AsyncPoolManager poolManager;

    async void Start()
    {
        await poolManager.InitializePoolAsync();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            poolManager.SpawnObject(transform.position, Quaternion.identity);
        }
    }
}