using UnityEngine;
using Unity.Cinemachine;

public class PlayerLook : MonoBehaviour
{
    [Header("Look Settings")]
     [SerializeField] private CinemachineInputAxisController _cameraInputAxisController;
    [SerializeField] private float _lookSensitivity = 5f;
    void Start()
    {
        SetLookSensitivity(_lookSensitivity);
    }
    private void SetLookSensitivity(float sensitivity)
    {                
        if (_cameraInputAxisController == null)
        {
            Debug.LogWarning("Camera Input Axis Controller is not assigned.");
            return;
        }

        // Pan Axis (X)
        _cameraInputAxisController.Controllers[0].Input.Gain = sensitivity;

        // Tilt Axis (Y)
        _cameraInputAxisController.Controllers[1].Input.Gain = -sensitivity;                
    } 
}
