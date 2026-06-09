using UnityEngine;
using UnityEngine.UI;

public class BatteryLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private Image _batteryLevelFill;
    [SerializeField] private Color _highColor;
    [SerializeField] private Color _mediumColor;
    [SerializeField] private Color _lowColor;

    public void SetVisible(bool isVisible)
    {
        _uiObject?.SetActive(isVisible);
    }

    public void SetBatteryLevelFill(float currentValue, float maxValue)
    {
        if (_batteryLevelFill == null) return;
        
        _batteryLevelFill.fillAmount = currentValue / maxValue;

        float batteryFillAmount = _batteryLevelFill.fillAmount;
        SetBatteryLevelColor(batteryFillAmount);
    }

    private void SetBatteryLevelColor(float batteryLevel)
    {
        if (batteryLevel > 0.75f)
        {
            _batteryLevelFill.color = _highColor;
        }
        else if (batteryLevel > 0.25f && batteryLevel < 0.75f)
        {
            _batteryLevelFill.color = _mediumColor;
        }
        else if (batteryLevel < 0.25f)
        {
            _batteryLevelFill.color = _lowColor;
        }
    }
}
