using UnityEngine;
using Unity.Cinemachine;
using System;

public class PlayerController : MonoBehaviour
{
    // Scripts References
    private InputProvider _inputProvider;
    private PlayerStamina _playerStamina;  

    [Header("Component References")]    
    [SerializeField] private Transform _mainCameraTransform;
    [SerializeField] private CharacterController _characterController;

    [Header("Movement Settings")]    
    [SerializeField] private float _walkSpeed = 1f; 
    [SerializeField] private float _sprintSpeed = 2;    
    [SerializeField] private float _acceleration = 0.3f;
    [SerializeField] private float _decceleration = 0.3f;
    private float _currentSpeed;             

    private Vector3 _horizontalVelocity;
    private Vector3 _cameraRelativeDirection;
    private bool _isSprinting;    

    void Start()
    {       
        _inputProvider = InputProvider.Instance;
        BindEvents();

        _playerStamina = GetComponent<PlayerStamina>();                 
    }
    private void OnDestroy()
    {
        UnbindEvents();
    }    

    void Update()
    {
        CalculateAcceleration();
        HandleMovement();
    }
    
    #region Input
    private void BindEvents()
    {
        _inputProvider.OnMoveEvent += ReadMoveInput;
        _inputProvider.OnSprintEvent += ReadSprintInput;
    }
    private void UnbindEvents()
    {
        _inputProvider.OnMoveEvent -= ReadMoveInput;
        //_inputProvider.OnRunEvent -= HandleRun;
    }
    private void ReadMoveInput(Vector2 movementInput)
    {
        _horizontalVelocity = new Vector3(movementInput.x, 0, movementInput.y);        
    }
    private void ReadSprintInput(bool isSprinting)
    {
        _isSprinting = isSprinting;
    }
    #endregion
    #region Movement
    private bool IsFacingForward()
    {
        float forwardAlignment = Vector3.Dot(_mainCameraTransform.forward, _cameraRelativeDirection);

        float forwardThreshold = 0.71f;
        return forwardAlignment > forwardThreshold;
    }

    public bool IsSprinting()
    {
        return _isSprinting && IsFacingForward();
    }

    private void CalculateAcceleration()
    {
        if (_horizontalVelocity.sqrMagnitude < 0.01f)
        {            
            _currentSpeed = 0;
            return;
        }

        if (_isSprinting && _playerStamina.CanSprint() && IsFacingForward())
        {
            _currentSpeed += _acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _decceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, _walkSpeed, _sprintSpeed);          
    }
    private void HandleMovement()
    {
        if (_horizontalVelocity.sqrMagnitude > 0.1f)
        {          
            Vector3 forward = _mainCameraTransform.forward;
            Vector3 right = _mainCameraTransform.right;
            
            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            _cameraRelativeDirection = forward * _horizontalVelocity.z + right * _horizontalVelocity.x;            

            Vector3 horizontalMove = _cameraRelativeDirection * _currentSpeed * Time.deltaTime;  
            _characterController.Move(horizontalMove);
        }
    }
    #endregion
}
