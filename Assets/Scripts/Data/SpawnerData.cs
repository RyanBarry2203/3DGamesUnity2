using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewSpawnerData", menuName = "Game Data/Spawner Data")]
public class SpawnerData : ScriptableObject
{
    [Header("Addressable Settings")]
    public AssetReference prefabReference;

    [Header("Spawning Settings")]
    public int spawnCountPerFrame = 5;
    public float spawnRadius = 10f;


    public int maxActiveObjects = 500;
    public InputActionReference toggleAction;
}