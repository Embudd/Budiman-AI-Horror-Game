using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public event Action<float, float> OnStaminaChanged; 

    private PlayerController _playerController;

    [Header("Stamina Settings")]
    private float _currentStamina;
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _sprintStaminaCost = 5f; 
    [SerializeField] private float _sprintStaminaRegen = 5f;
    [SerializeField] private float _regenCooldown = 2f;
    private float _regenTimer;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();    
        _currentStamina = _maxStamina; 
    }

    void Update()
    {
        CalculateStamina();
    }

    // Stamina Regen Cooldown so when the player stop moving the stamina will not constantly regen, 
    // need to wait for a seconds before the stamina start to regen
    private bool IsRegenCooldownFinished()
    {
        if (!_playerController.IsSprinting())
        {
            _regenTimer += Time.deltaTime;
            return _regenTimer >= _regenCooldown;
        }        

        return false;
    }

    private void ResetTimer()
    {
        _regenTimer = 0f;
    }
    
    private void CalculateStamina()
    {
        if (_playerController.IsSprinting())
        {
            _currentStamina -= _sprintStaminaCost * Time.deltaTime;
            ResetTimer();
        }
        else if (IsRegenCooldownFinished())
        {
            _currentStamina += _sprintStaminaRegen * Time.deltaTime;
        }

        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
        OnStaminaChanged?.Invoke(_currentStamina, _maxStamina);
    }

    public bool CanSprint()
    {
        return _currentStamina > 0;
    }
}   
