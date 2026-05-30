using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _owner;
    [SerializeField] private Transform _mainCamera;

    [Header("Ligth Config")]
    [SerializeField] private Light _light;     
    [SerializeField] private float _initialBatteryLevel = 100;    
    [SerializeField] private float _batteryDrainRate = 1;    
    private float _batteryLevel;
    
    public bool HasBattery => _batteryLevel > 0;
    public bool hasFlashlight => _owner.Inventory.CheckItem("Flashlight_001");

    void OnEnable()
    {
        InputManager.Instance.OnFlashlightEvent += UseFlashlight;
    }

    void OnDestroy()
    {
        InputManager.Instance.OnFlashlightEvent -= UseFlashlight;
    }

    private void Awake() 
    {
        _batteryLevel = _initialBatteryLevel;    
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
            }
            else
            {     
                _batteryLevel = 0;
                _light.enabled = false;
            }
        }
    }
}

