using System.Collections;
using UnityEngine;

public class RotatingDoor : Door
{
    [Header("Open Config")]
    [SerializeField] private float _openAngle;
    [SerializeField] private float _closeAngle;
    [SerializeField] protected Collider _doorCollider;

    public override void Open()
    {
        if (_animatingDoorLeanTween != null)
        {
            LeanTween.cancel(_animatingDoorLeanTween.id);
        }

        _doorAudioEvent.DoorOpened(_audioSource);
        RotateDoor(_openAngle);
        base.Open();
    }

    public override void Close()
    {
        if (_animatingDoorLeanTween != null)
        {
            LeanTween.cancel(_animatingDoorLeanTween.id);
        }

        _doorAudioEvent.DoorClosed(_audioSource);
        RotateDoor(_closeAngle);
        base.Close();
    }

    public override void Locked()
    {
        _doorAudioEvent.DoorLocked(_audioSource);
    }

    private void RotateDoor(float targetRotate)
    {
        _isAnimating = true;
        _doorCollider.enabled = false;

        float startAngle = _doorTransform.localEulerAngles.y;

        float deltaAngle = Mathf.DeltaAngle(startAngle, targetRotate);

        float totalDistance = Mathf.Abs(_openAngle);
        float angleDistance = Mathf.Abs(deltaAngle);

        float normalizedDuration = (angleDistance / totalDistance) * _openDuration;
        
        float targetAngle = startAngle + deltaAngle;

        _animatingDoorLeanTween = LeanTween.value(startAngle, targetAngle, normalizedDuration)
                                    .setEase(_easingType)
                                    .setOnUpdate((float rot) =>
                                        _doorTransform.localRotation = Quaternion.Euler(0, rot, 0))
                                    .setOnComplete(OnAnimationComplete);
    }

    private void OnAnimationComplete()
    {
        _isAnimating = false;
        _doorCollider.enabled = true;
    }
}
