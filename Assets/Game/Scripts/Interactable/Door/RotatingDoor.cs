using System.Collections;
using UnityEngine;

public class RotatingDoor : Door
{
    [SerializeField] private float _openAngle;
    [SerializeField] private float _closeAngle;

    protected override void Open()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }

        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_openAngle));
        base.Open();
    }

    protected override void Close()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }

        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_closeAngle));
        base.Close();
    }

    private IEnumerator RotateDoor(float targetRotate)
    {
        _isAnimating = true;

        float startAngle = _doorTransform.localEulerAngles.y;
        float time = 0;

        while (time < openDuration)
        {
            time += Time.deltaTime;            
            float currentTargetRotation = Mathf.LerpAngle(startAngle, targetRotate, time / openDuration);
            _doorTransform.localRotation = Quaternion.Euler(0, currentTargetRotation, 0);

            yield return null;
        }

        _doorTransform.localRotation = Quaternion.Euler(0, targetRotate, 0);
        _isAnimating = false;
    }
}
