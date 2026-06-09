using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private Image _staminaFill;    

    public void SetVisible(bool isVisible)
    {
        _uiObject?.SetActive(isVisible);
    }

    public void SetStaminaFill(float currentValue, float maxValue)
    {
        if (_staminaFill == null) return;

        _staminaFill.fillAmount = currentValue / maxValue;
    }
}
