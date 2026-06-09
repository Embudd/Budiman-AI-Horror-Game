using System;
using UnityEngine;

public abstract class Door : MonoBehaviour, IInteractable
{
    [Header("Door Config")]
    [SerializeField] protected string _name;
    [SerializeField] protected Transform _doorTransform;
    [SerializeField] protected float _openDuration = 1.0f;
    [SerializeField] protected LeanTweenType _easingType;
    [SerializeField] protected bool _isLocked;
    [SerializeField] protected string _keyID;

    protected bool _isAnimating;    
    protected bool _isOpen;

    protected LTDescr _animatingDoorLeanTween;

    public string Name => _name;
    public bool IsAnimating => _isAnimating;

    public event Action OnDoorOpened;
    public event Action OnDoorClosed;

    [ContextMenu("Open Door")]
    public void Interact(PlayerCharacter character)
    {
        if (_isLocked)
        {
            bool hasKey = character.Inventory.CheckItem(_keyID);

            if(hasKey)
            {
                _isLocked = false;
                Open();
            }
        }
        else
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
    }

    public virtual void Open()
    {
        _isOpen = true;
        OnDoorOpened?.Invoke();                
    }

    public virtual void Close()
    {
        _isOpen = false;
        OnDoorClosed?.Invoke();        
    }

    void OnDestroy()
    {
        LeanTween.reset();
    }
}