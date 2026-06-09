using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set;}

    [SerializeField] private PlayerStaminaUI _staminaUI;
    [SerializeField] private BatteryLevelUI _batteryUI;
    [SerializeField] private InteractionInfoUI _interactionInfoUI;
    [SerializeField] private CrosshairUI _crosshairUI;

    public PlayerStaminaUI StaminaUI => _staminaUI;
    public BatteryLevelUI BatteryUI => _batteryUI;
    public InteractionInfoUI InteractionInfoUI => _interactionInfoUI;
    public CrosshairUI CrosshairUI => _crosshairUI;

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
