using System;
using UnityEngine;

public abstract class Door : MonoBehaviour, IInteractable
{
    [Header("Door Config")]
    [SerializeField] protected string _name;
    [SerializeField] protected Transform _doorTransform;
    [SerializeField] protected float _openDuration = 1.0f;
    [SerializeField] protected string _keyID;
    protected bool _isAnimating;
    protected bool _isLocked;
    protected bool _isOpen;

    protected Coroutine _animatingDoorCoroutine;

    public string Name => _name;

    public event Action OnDoorOpened;
    public event Action OnDoorClosed;

    [ContextMenu("Open Door")]
    public void Interact(PlayerCharacter character)
    {
        if (_isOpen != true)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    protected virtual void Open()
    {
        _isOpen = true;
        OnDoorOpened?.Invoke();        
    }

    protected virtual void Close()
    {
        _isOpen = false;
        OnDoorClosed?.Invoke();        
    }
}