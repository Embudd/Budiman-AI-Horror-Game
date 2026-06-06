using UnityEngine;

public class ShowGhostEvent : GameEventBase
{
    [SerializeField] private GameObject _ghostObject;
    [SerializeField] private bool _isDestroyAfterTrigger;

    public override void Trigger()
    {        
        if (_ghostObject != null)
        {
            _ghostObject.SetActive(true);
        }

        base.Trigger();
    }

    public override void Finish()
    {        
        if (_ghostObject != null && _isDestroyAfterTrigger)
        {
            Destroy(_ghostObject);
        }

        base.Finish();
    }
}
