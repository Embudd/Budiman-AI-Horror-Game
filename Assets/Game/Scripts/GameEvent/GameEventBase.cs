using UnityEngine;
using System;

public abstract class GameEventBase : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private bool _isOneTime; 

    public event Action OnEventTriggered;
    public event Action OnEventFinished;

    public string ID => _id;

    public void Start()
    {
        GameEventManager.Instance.RegisterEvent(this);    
    }

    public virtual void Trigger()
    {
        OnEventTriggered?.Invoke();
    }

    public virtual void Finish()
    {
        OnEventFinished?.Invoke();

        if (_isOneTime)
        {
            GameEventManager.Instance.UnregisterEvent(this);
            Destroy(gameObject);
        }
    }
}
