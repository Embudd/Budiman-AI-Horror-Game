using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlidingDoor : Door
{
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

        SlideDoor(_openPosition);
        base.Open();
    }

    public override void Close()
    {
        if (_animatingDoorLeanTween != null)
        {
            LeanTween.cancel(_animatingDoorLeanTween.id);
        }

        
        SlideDoor(_closePosition);
        base.Close();
    }

    // private IEnumerator SlideDoor(Vector3 targetPosition)
    // {
    //     _isAnimating = true;

    //     Vector3 startPosition = _doorTransform.localPosition;
    //     float time = 0f;
        
        
    //     while (time < _openDuration)
    //     {            
    //         time = time + Time.deltaTime;
    //         Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / _openDuration);            
    //         _doorTransform.localPosition = position;            

    //         yield return null;
    //     }
        
    //     _doorTransform.localPosition = targetPosition;
    //     _isAnimating = false;
    // }

    private void SlideDoor(Vector3 targetPosition)
    {
        _isAnimating = true;

        Vector3 startPosition = _doorTransform.localPosition;

        // Get the distance from start to target
        float distance = Vector3.Distance(startPosition, targetPosition);

        // noramlized the duration based on how far the distance is.
        float normalizedDuration = (distance / _maxOpenDistance) * _openDuration;

        _animatingDoorLeanTween = LeanTween.moveLocal(gameObject, targetPosition, normalizedDuration)
                                    .setEase(_easingType)
                                    .setOnComplete(() => _isAnimating = false);    
    }
}
