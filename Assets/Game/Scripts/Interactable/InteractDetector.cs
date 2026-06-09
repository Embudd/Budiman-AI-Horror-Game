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

    public bool Enabled { get; private set; } = true;     
        
    private IInteractable _detectedInteractable;
    private bool _isInteracting;    

    void Start()
    {
        InputManager.Instance.OnInteractEvent += Interact;
    }

    void OnDestroy()
    {
        InputManager.Instance.OnInteractEvent -= Interact;
    }

    void Update()
    {
        UpdateDetection();
    }

    public void SetEnabled(bool isEnabled)
    {
        Enabled = isEnabled;
    }

    private void UpdateDetection()
    {            
        if (_isInteracting)
        {            
            _isInteracting = false;
            return;
        }

        if (Enabled == false) return;        

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

                HUDManager.Instance.InteractionInfoUI.SetItemNameText(_detectedInteractable.Name);
                HUDManager.Instance.InteractionInfoUI.SetVisible(true);
            }
        }
        else
        {
            _detectedInteractable = null;

            HUDManager.Instance.InteractionInfoUI.SetVisible(false);
        }        
    }

    public void Interact()
    {        
        if (_detectedInteractable == null || Enabled == false) return;
        
        _detectedInteractable.Interact(_owner);
        _detectedInteractable = null;        
        _isInteracting = true;

        HUDManager.Instance.InteractionInfoUI.SetVisible(false);
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

        if (Enabled == false) return;
        
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
