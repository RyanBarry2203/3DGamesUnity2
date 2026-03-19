using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Game Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 2f;
    public float crouchHeight = 1f;
    public float jumpHeight = 3f;
    public float gravity = 9.81f;

    [Header("Look Settings")]
    public float horizontalSensitivity = 10f;
    public float verticalSensitivity = 10f;
    public bool isCursorVisible = false;

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public LayerMask interactionLayers;

    [Header("Combat Settings")]
    public float weaponDistance = 3f;
    public float weaponDamage = 25f;
    public LayerMask damageableLayers;
}