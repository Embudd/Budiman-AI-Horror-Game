using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set;}

    [SerializeField] private PlayerStaminaUI _staminaUI;
    [SerializeField] private BatteryLevelUI _batteryUI;
    [SerializeField] private InteractionInfoUI _interactionInfoUI;

    public PlayerStaminaUI StaminaUI => _staminaUI;
    public BatteryLevelUI BatteryUI => _batteryUI;
    public InteractionInfoUI InteractionInfoUI => _interactionInfoUI;

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
