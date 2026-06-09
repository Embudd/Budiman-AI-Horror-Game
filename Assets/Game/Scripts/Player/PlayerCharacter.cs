using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public event Action<string> OnPlayerDeath;

    [SerializeField] private PlayerCharacterMovement _movement;
    [SerializeField] private PlayerCharacterStamina _stamina;
    [SerializeField] private InteractDetector _interactDetector;
    [SerializeField] private InventoryManager _inventory;
    [SerializeField] private CameraManager _camera;
    [SerializeField] private Flashlight _flashLight;

    public PlayerCharacterMovement Movement => _movement;
    public PlayerCharacterStamina Stamina => _stamina;
    public InteractDetector InteractDetector => _interactDetector;
    public InventoryManager Inventory => _inventory;
    public CameraManager Camera => _camera;
    public Flashlight Flashlight => _flashLight;

    public bool IsHiding { get; private set; }
    
    private void Start()
    {
        DisplayCursor.Instance.LockCursor();
    } 
     
    public void SetIsHiding(bool isHiding)
    {
        IsHiding = isHiding;
    }

    public void Death()
    {        
        DisplayCursor.Instance.UnlockCursor();
        OnPlayerDeath?.Invoke("LoseScreen");
    }    
}
