using Unity.Cinemachine;
using UnityEngine;

public class CameraViewBobbing : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private PlayerCharacterMovement _movement;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _cmPerlinNoise;

    [Header("View Bobbing Config")]
    [SerializeField] private float _walkBobspeed = 1.0f;
    [SerializeField] private float _smoothingSpeed = 3.0f;
    [SerializeField] private float _walkBobAmountX = 0.02f;
    [SerializeField] private float _walkBobAmountY = 0.02f;
    [SerializeField] private float _runBobSpeed = 3f;
    [SerializeField] private float _runBobAmountX = 0.08f;
    [SerializeField] private float _runBobAmountY = 0.08f;

    private Vector3 _startPos;
    private float _timer;

    void Start()
    {
        _startPos = transform.localPosition;
    }

    void Update()
    {        
        UpdateBobbing(_movement._currentState);
    }

    private void UpdateBobbing(PlayerState state)
    {
        Vector3 manualBobPos = Vector3.zero;
        if (state == PlayerState.Walk || state == PlayerState.Run)
        {
            float currentSpeed = (state == PlayerState.Run) ? _runBobSpeed : _walkBobspeed;
            float amountX = (state == PlayerState.Run) ? _runBobAmountX : _walkBobAmountX;
            float amountY = (state == PlayerState.Run) ? _runBobAmountY : _walkBobAmountY;

            _timer += Time.deltaTime * currentSpeed;
            float newX = Mathf.Sin(_timer) * amountX;
            float newY = Mathf.Cos(_timer * 2f) * amountY;
            manualBobPos = new Vector3(newX, newY, 0);
        }
        
        float targetGain = (state == PlayerState.Idle) ? 1.0f : 0.0f;
        _cmPerlinNoise.AmplitudeGain = Mathf.Lerp(_cmPerlinNoise.AmplitudeGain, targetGain, Time.deltaTime * _smoothingSpeed);

        Vector3 targetPos = _startPos + manualBobPos;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * _smoothingSpeed);
    }
}

