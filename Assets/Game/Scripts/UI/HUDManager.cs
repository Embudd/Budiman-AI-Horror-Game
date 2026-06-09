using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set;}

    [SerializeField] private PlayerStaminaUI _staminaUI;
    [SerializeField] private BatteryLevelUI _batteryUI;

    public PlayerStaminaUI StaminaUI => _staminaUI;
    public BatteryLevelUI BatteryUI => _batteryUI;

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
    }
}
