using System;
using System.Collections;
using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{    
    [SerializeField] private PlayerCharacterMovement _playerController;

    [Header("Stamina Settings")]
    private float _currentStamina;
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _sprintStaminaCost = 5f; 
    [SerializeField] private float _sprintStaminaRegen = 5f;
    [SerializeField] private float _regenCooldown = 2f;
    private float _regenTimer;
    private Coroutine _stopRegenStaminaCoroutine;
    private bool _isWaitingRegenStamina;

    private WaitForSeconds _stopRegenStaminaWaitInterval = new WaitForSeconds(1.5f);
    
    private void Start()
    {
        _currentStamina = _maxStamina; 

        HUDManager.Instance.StaminaUI.SetVisible(false);
        HUDManager.Instance.StaminaUI.SetStaminaFill(_currentStamina, _maxStamina);
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
       
    public bool CanSprint()
    {
        return _currentStamina > 0;
    }
    
    private void CalculateStamina()
    {
        if (_playerController.IsSprinting())
        {
            if (_stopRegenStaminaCoroutine != null)
            {
                StopCoroutine(_stopRegenStaminaCoroutine);
                _stopRegenStaminaCoroutine = null;
            }

            _isWaitingRegenStamina = false;

            _currentStamina -= _sprintStaminaCost * Time.deltaTime;
            ResetTimer();
        }
        else if (IsRegenCooldownFinished())
        {
            if (_currentStamina < _maxStamina)
            {
                _currentStamina += _sprintStaminaRegen * Time.deltaTime;    
            }
            else if (_isWaitingRegenStamina == false)
            {
                _stopRegenStaminaCoroutine = StartCoroutine(StopRegenStaminaWait());
                _isWaitingRegenStamina = true;
            }
        }

        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);        
        HUDManager.Instance.StaminaUI.SetStaminaFill(_currentStamina, _maxStamina);
    }

    private IEnumerator StopRegenStaminaWait()
    {
        yield return _stopRegenStaminaWaitInterval;

        HUDManager.Instance.StaminaUI.SetVisible(false);

        _stopRegenStaminaCoroutine = null;
        _isWaitingRegenStamina = false;
    }
}
