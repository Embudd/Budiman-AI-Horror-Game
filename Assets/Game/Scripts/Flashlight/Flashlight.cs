using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _owner;
    [SerializeField] private Transform _mainCamera;

    [Header("Battery Item")]
    [SerializeField] private Item _batteryItem;

    [Header("Ligth Config")]
    [SerializeField] private Light _light;     
    [SerializeField] private float _initialBatteryLevel = 100;    
    [SerializeField] private float _batteryDrainRate = 1;    
    private float _batteryLevel;
    
    public bool HasBattery => _batteryLevel > 0;
    public bool hasFlashlight => _owner.Inventory.CheckItem("Flashlight_001");    
    
    private void Awake() 
    {
        _batteryLevel = _initialBatteryLevel;    
    }

    private void Start()
    {
        InputManager.Instance.OnFlashlightEvent += UseFlashlight;
        _batteryItem.OnItemPicked += RefillBatteryLevel;

        HUDManager.Instance.BatteryUI.SetBatteryLevelFill(_batteryLevel, _initialBatteryLevel);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnFlashlightEvent -= UseFlashlight;
        _batteryItem.OnItemPicked -= RefillBatteryLevel;
    }

    private void Update() 
    {        
        UpdateFlashlightRotation();        
        UpdateBatteryLevel();
    }

    public void UseFlashlight()
    {        
        if (hasFlashlight == true && _light != null)
        {
            if (HasBattery == true)
            {                
                _light.enabled = !_light.enabled;
            }
            else
            {            
                _light.enabled = false;
            }
        }
    }

    private void UpdateFlashlightRotation()
    {
        _light.transform.rotation = _mainCamera.transform.rotation;
    }
    
    private void UpdateBatteryLevel()
    {
        if (_light != null && _light.enabled == true)
        {
            if (HasBattery == true)
            {
                _batteryLevel -= _batteryDrainRate * Time.deltaTime;
                _batteryLevel = Mathf.Clamp(_batteryLevel, 0f, _initialBatteryLevel);
            }
            else
            {
                _batteryLevel = 0f;
                _light.enabled = false;
            }
        }

        HUDManager.Instance.BatteryUI.SetBatteryLevelFill(_batteryLevel, _initialBatteryLevel);
    }

    public void SetBatteryLevel(float batteryLevel)
    {
        _batteryLevel = _batteryLevel + batteryLevel;
        _batteryLevel = Mathf.Clamp(_batteryLevel, 0, _initialBatteryLevel);
        HUDManager.Instance.BatteryUI.SetBatteryLevelFill(_batteryLevel, _initialBatteryLevel);
    }

    public void RefillBatteryLevel()
    {
        _batteryLevel = _initialBatteryLevel;
        HUDManager.Instance.BatteryUI.SetBatteryLevelFill(_batteryLevel, _initialBatteryLevel);
    }
}

