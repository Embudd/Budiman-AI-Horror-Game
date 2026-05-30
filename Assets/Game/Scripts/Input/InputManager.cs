using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameInputAction;

public class InputManager : MonoBehaviour, IPlayerActions
{
    public static InputManager Instance { get; private set; }

    private GameInputAction _inputActions;

    public event Action<Vector2> OnMoveEvent;
    public event Action<bool> OnSprintEvent;
    public event Action OnInteractEvent;
    public event Action OnFlashlightEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;         
        }
        else
        {
            Destroy(gameObject);            
        }

        _inputActions = new GameInputAction();         
    }

    private void OnEnable()
    {        
        _inputActions.Player.Enable();
        _inputActions.Player.SetCallbacks(this);
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        _inputActions.Player.SetCallbacks(null);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractEvent?.Invoke();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSprintEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnSprintEvent?.Invoke(false);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Look input already handled by Cinecmachine
    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnFlashlightEvent?.Invoke();
        }
    }
}
