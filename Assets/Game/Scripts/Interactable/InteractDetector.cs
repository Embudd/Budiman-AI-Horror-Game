using Unity.Mathematics;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    [Header("Detection Config")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private PlayerCharacter _owner;
    [SerializeField] private Vector3 _detectorBoxSize;
    [SerializeField] private float _detectorDistance;
    [SerializeField] private LayerMask _interactableLayer;
    
    private IInteractable _detectedInteractable;
    private bool _isInteracting;    

    void Start()
    {
        InputProvider.Instance.OnInteractEvent += Interact;
    }

    void OnDestroy()
    {
        InputProvider.Instance.OnInteractEvent += Interact;
    }

    void Update()
    {
        UpdateDetection();
    }

    private void UpdateDetection()
    {        
        if (_isInteracting)
        {            
            _isInteracting = false;
            return;
        }
                
        bool isDetectingInteractable = Physics.BoxCast(_cameraTransform.position,
                                                        _detectorBoxSize * 0.5f,
                                                        _cameraTransform.forward,
                                                        out RaycastHit hit,
                                                        Quaternion.identity,
                                                        _detectorDistance,
                                                        _interactableLayer
                                                        );
        
        if (isDetectingInteractable)
        {
            if(hit.collider.TryGetComponent(out IInteractable interactable))            
            {
                _detectedInteractable = interactable;        
            }
        }
        else
        {
            _detectedInteractable = null;
        }

        Debug.Log(_detectedInteractable);
    }

    public void Interact()
    {        
        if (_detectedInteractable == null) return;
        
        _detectedInteractable.Interact(_owner);
        _detectedInteractable = null;        
        _isInteracting = true;
    }

     private void OnDrawGizmosSelected()
    {        
        Gizmos.color = Color.red;        
        Transform cameraTransform = Camera.main.transform;
        bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position,
                                                       _detectorBoxSize * 0.5f,
                                                       cameraTransform.forward,
                                                       out RaycastHit hit,
                                                       Quaternion.identity,                                                                _detectorDistance,
                                                        _interactableLayer
                                                        );
        if (isDetectingInteractable)
        {     
            Gizmos.color = Color.green;         
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position +
                            cameraTransform.forward * hit.distance);            
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward *
                                hit.distance, _detectorBoxSize);
        }
        else
        {            
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position +
                            cameraTransform.forward * _detectorDistance);         
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward *
                               _detectorDistance, _detectorBoxSize);   
        }
    }
}
