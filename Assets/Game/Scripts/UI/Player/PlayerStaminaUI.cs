using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class PlayerStaminaUI : MonoBehaviour
{
    [Header("ComponentReferences")]
    [SerializeField]private PlayerCharacterStamina _playerStamina;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI _staminaFillText;
    
    private StringBuilder _staminaTextBuilder = new StringBuilder();

    private void OnEnable()
    {
        _playerStamina.OnStaminaChanged += UpdateStaminaUI;
    }

    private void OnDisable()
    {
        _playerStamina.OnStaminaChanged -= UpdateStaminaUI;
    }

    private void UpdateStaminaUI(float currentStamina, float maxStamina)
    {        
        _staminaTextBuilder.Clear();
        _staminaTextBuilder.Append($"{currentStamina:F0}/{maxStamina:F0}");

        _staminaFillText.text = _staminaTextBuilder.ToString();
    }
}
