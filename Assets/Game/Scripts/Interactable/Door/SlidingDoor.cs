using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private Vector3 _closePosition;

    public override void Open()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_openPosition));

        base.Open();
    }

    public override void Close()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_closePosition));

        base.Close();
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        IsAnimating = true;

        Vector3 startPosition = _doorTransform.localPosition;
        float time = 0f;
        
        
        while (time < _openDuration)
        {            
            time = time + Time.deltaTime;
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / _openDuration);            
            _doorTransform.localPosition = position;            

            yield return null;
        }
        
        _doorTransform.localPosition = targetPosition;
        IsAnimating = false;
    }
}
