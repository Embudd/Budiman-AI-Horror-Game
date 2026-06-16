using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private Door[] _rotatingDoors;
    [SerializeField] private AudioSource _doorOpened;
    [SerializeField] private AudioSource _doorClosed;
    [SerializeField] private AudioSource _doorLocked;

    private void Start()
    {
        RegisterRotatingDoor();
    }

    private void OnDestroy()
    {
        UnregisterRotatingDoor();
    }

    private void RegisterRotatingDoor()
    {
        foreach (var door in _rotatingDoors)
        {
            door.OnDoorOpened += TriggerOpenSFX;
            door.OnDoorClosed += TriggerCloseSFX;
            door.OnDoorLocked += TriggerLockSFX;
        }
    }

    private void UnregisterRotatingDoor()
    {
        foreach (var door in _rotatingDoors)
        {
            door.OnDoorOpened -= TriggerOpenSFX;
            door.OnDoorClosed -= TriggerCloseSFX;
            door.OnDoorLocked -= TriggerLockSFX;
        }
    }

    private void TriggerOpenSFX()
    {
        _doorOpened.Play();
    }

    private void TriggerCloseSFX()
    {
        _doorClosed.Play();
    }

    private void TriggerLockSFX()
    {
        _doorLocked.Play();
    }
}
