using System;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{    
    [SerializeField] private string _eventID;
    [SerializeField] private bool _autoActive;
    [SerializeField] private string _tag;
    [SerializeField] private bool _isOneTime;

    private bool _isActive;    

    private void Awake()
    {
        _isActive = _isOneTime;
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag) && _isActive)
        {            
            if (_isOneTime)
            {
                GameEventManager.Instance.TriggerEvent(_eventID);
                Destroy(gameObject);
            }
        }
    }
}
