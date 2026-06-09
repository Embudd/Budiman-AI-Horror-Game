using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatingDoor : Door
{
    [SerializeField] private float _openAngle;
    [SerializeField] private float _closeAngle;

    public override void Open()
    {
        if (_animatingDoorLeanTween != null)
        {
            LeanTween.cancel(_animatingDoorLeanTween.id);
        }

        RotateDoor(_openAngle);
        base.Open();
    }

    public override void Close()
    {
        if (_animatingDoorLeanTween != null)
        {
            LeanTween.cancel(_animatingDoorLeanTween.id);
        }

        RotateDoor(_closeAngle);
        base.Close();
    }

    private void RotateDoor(float targetRotate)
    {
        _isAnimating = true;

        float startAngle = _doorTransform.localEulerAngles.y;
        
        // Get the angle to reach from start to target
        float deltaAngle = Mathf.DeltaAngle(startAngle, targetRotate);
                        
        float totalDistance = _openAngle;
        float angleDistance = Mathf.Abs(deltaAngle); // Abs it so it never be minus for time calculation

        // normalized time based on how far the angle from start to target 
        float normalizedDuration = (angleDistance / totalDistance) * _openDuration;

        _animatingDoorLeanTween =  LeanTween.value(startAngle, targetRotate, normalizedDuration)
                                    .setEase(_easingType)
                                    .setOnUpdate((float rot) => 
                                    _doorTransform.localRotation = Quaternion.Euler(0, rot, 0))
                                    .setOnComplete(() => _isAnimating = false);

        
    }
}
