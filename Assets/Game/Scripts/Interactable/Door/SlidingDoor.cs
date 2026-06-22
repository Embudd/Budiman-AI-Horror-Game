using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlidingDoor : Door
{
    [Header("Open Config")]
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private Vector3 _closePosition;
    private float _maxOpenDistance;

    private void Start()
    {
        _maxOpenDistance = Vector3.Distance(_closePosition, _openPosition);
    }

    public override void Open()
    {
        if (_animatingDoorLeanTween != null)
        {
            LeanTween.cancel(_animatingDoorLeanTween.id);
        }

        _doorAudioEvent.DoorOpened(_audioSource);
        SlideDoor(_openPosition);
        base.Open();
    }

    public override void Close()
    {
        if (_animatingDoorLeanTween != null)
        {
            LeanTween.cancel(_animatingDoorLeanTween.id);
        }

        _doorAudioEvent.DoorClosed(_audioSource);
        SlideDoor(_closePosition);
        base.Close();
    }

    public override void Locked()
    {
         _doorAudioEvent.DoorLocked(_audioSource);
    }

    private void SlideDoor(Vector3 targetPosition)
    {
        _isAnimating = true;

        Vector3 startPosition = _doorTransform.localPosition;

        // Get the distance from start to target
        float distance = Vector3.Distance(startPosition, targetPosition);

        // noramlized the duration based on how far the distance is.
        float normalizedDuration = (distance / _maxOpenDistance) * _openDuration;

        _animatingDoorLeanTween = LeanTween.moveLocal(_doorTransform.gameObject, targetPosition, normalizedDuration)
                                    .setEase(_easingType)
                                    .setOnComplete(() => _isAnimating = false);
    }
}
