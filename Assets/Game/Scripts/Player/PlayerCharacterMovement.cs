using UnityEngine;
using Unity.Cinemachine;
using System;

public class PlayerCharacterMovement : MonoBehaviour
{
    // Scripts References
    private InputManager _inputProvider;
    private PlayerCharacterStamina _playerStamina;  

    [Header("Component References")]    
    [SerializeField] private Transform _mainCameraTransform;
    [SerializeField] private CharacterController _characterController;

    [Header("Movement Settings")]    
    [SerializeField] private float _walkSpeed = 1f; 
    [SerializeField] private float _sprintSpeed = 2;    
    [SerializeField] private float _acceleration = 0.3f;
    [SerializeField] private float _decceleration = 0.3f;
    private float _currentSpeed;             
    public bool Enabled { get; private set; } = true;         

    [Header("Gravity Settings")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _gravityAcceleration;
    [SerializeField] private float _minGravitySpeed;
    [SerializeField] private float _maxGravitySpeed;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;

    private Vector3 _velocityXZ; // Raw input direction
    private Vector3 _cameraRelativeDirection; // Camera calculate direction
    private float _velocityY;    
    private bool _isSprinting;    

    void Start()
    {       
        _inputProvider = InputManager.Instance;
        BindEvents();

        _playerStamina = GetComponent<PlayerCharacterStamina>();                 
    }
    private void OnDestroy()
    {
        UnbindEvents();
    }    

    void Update()
    {
        CalculateFinalMovement();
    }

    public void SetEnabled(bool isEnabled)
    {
        Enabled = isEnabled;
    }

    private void CalculateFinalMovement()
    {
        if (Enabled == false) return;

        CalculateGravity();
        CalculateAcceleration();
        CalculateCameraRelativeDirection();

        Vector3 cameraRelative = _cameraRelativeDirection * _currentSpeed;                
        Vector3 finalMovement = new Vector3(cameraRelative.x, _velocityY, cameraRelative.z);                

        _characterController.Move(finalMovement * Time.deltaTime);                
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
        _inputProvider.OnSprintEvent -= ReadSprintInput;
    }
    private void ReadMoveInput(Vector2 movementInput)
    {
        _velocityXZ = new Vector3(movementInput.x, 0, movementInput.y);        
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
        if (_isSprinting)
        {
            HUDManager.Instance.StaminaUI.SetVisible(true);
        }

        return _isSprinting && IsFacingForward();
    }

    private void CalculateAcceleration()
    {
        if (_velocityXZ.sqrMagnitude < 0.01f)
        {            
            _currentSpeed = 0;
            return;
        }

        if (IsSprinting() && _playerStamina.CanSprint())
        {
            _currentSpeed += _acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _decceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, _walkSpeed, _sprintSpeed);          
    }
    private void CalculateCameraRelativeDirection()
    {
        if (_velocityXZ.sqrMagnitude > 0.1f)
        {          
            Vector3 forward = _mainCameraTransform.forward;
            Vector3 right = _mainCameraTransform.right;
            
            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            _cameraRelativeDirection = forward * _velocityXZ.z + right * _velocityXZ.x;            
        }
    }
    #endregion
    #region Gravity
    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, _groundCheckRadius, _groundLayer);
    }
    private void CalculateGravity()
    {
        // If player grounded set gravity to -2
        if (IsGrounded() && _velocityY < 0)
        {
            _velocityY = -2;
        }
        else if (!IsGrounded()) // Increase gravity if player not grounded
        {
            _velocityY += _gravity * _gravityAcceleration * Time.deltaTime;             
        }        

        _velocityY = Mathf.Clamp(_velocityY, _maxGravitySpeed, _minGravitySpeed);
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
    }
    #endregion
}
