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
    [SerializeField] protected ItemID _keyID;

    [Header("Door Audio")]
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected DoorAudioEvent _doorAudioEvent;

    protected LTDescr _animatingDoorLeanTween;
    protected bool _isAnimating;    
    protected bool _isOpen;    

    public string Name => _name;
    public bool IsAnimating => _isAnimating;  
    
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
            else
            {
                Locked();
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
    }

    public virtual void Close()
    {
        _isOpen = false;        
    }

    public abstract void Locked();

    void OnDestroy()
    {
        LeanTween.reset();
    }
}