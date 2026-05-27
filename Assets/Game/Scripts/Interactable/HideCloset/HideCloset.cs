using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HideCloset : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;    
    [SerializeField] private Transform _hidePosition;    
    [SerializeField] private Transform _unhidePosition;    
    [SerializeField] private float _duration = 1;            
    [SerializeField] private Door _door;    
             
    private PlayerCharacter _hidingPlayer;
    private Coroutine _hideCoroutine;
    private Coroutine _unhideCoroutine;

    public bool IsHiding { get; private set; }
    public string Name => _name;
  
    public void Interact(PlayerCharacter character)
    {
        if (_hidePosition != null && _unhidePosition != null && _door != null)
        {
            _hidingPlayer = character;
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
            }
            StartCoroutine(Hide());
        }
    }    
        
    public void StopHiding()
    {    
        if (_unhideCoroutine != null)
        {     
            StopCoroutine(_unhideCoroutine);
        }        
        StartCoroutine(Unhide());
    }
      
    public void SetIsHiding(bool isHiding)
    {
        IsHiding = isHiding;
    }

    public IEnumerator Hide()
    {
        
        SetIsHiding(true);        
        _hidingPlayer.Camera.SetCameraInputEnabled(false);        
        _hidingPlayer.Movement.SetEnabled(false);        
        _hidingPlayer.InteractDetector.SetEnabled(false);        
        _hidingPlayer.Camera.ResetCameraRotation();
         
        _door.Open();
 
        yield return new WaitWhile(() => _door.IsAnimating);
  
        float time = 0f;
        Vector3 startPosition = _hidingPlayer.transform.position;        
        float startRotation = _hidingPlayer.Camera.PanAxis;        
        while (time < _duration)
        {            
            time = time + Time.deltaTime;         
            _hidingPlayer.transform.position = Vector3.Lerp(startPosition, _hidePosition.position, time / _duration);            
            float panAxis = Mathf.Lerp(startRotation, _hidePosition.eulerAngles.y, time / _duration);            
            _hidingPlayer.Camera.SetPanAxisValue(panAxis);            
            yield return null;
        }
    
        _hidingPlayer.transform.position = _hidePosition.position;    
        _hidingPlayer.transform.rotation = _hidePosition.rotation;
         
        _door.Close();
  
        yield return new WaitWhile(() => _door.IsAnimating);
        InputProvider.Instance.OnInteractEvent += StopHiding;
    }

     public IEnumerator Unhide()
    {        
        InputProvider.Instance.OnInteractEvent -= StopHiding;

        _door.Open();        
        yield return new WaitWhile(() => _door.IsAnimating);
         
        float time = 0f;

        Vector3 startPosition = _hidingPlayer.transform.position;        
        float startRotation = _hidingPlayer.Camera.PanAxis;
        
        while (time < _duration)
        {            
            time = time + Time.deltaTime;            
            _hidingPlayer.transform.position = Vector3.Lerp(startPosition, _unhidePosition.position, time / _duration);            
            float panAxis = Mathf.Lerp(startRotation, _unhidePosition.rotation.y, time / _duration);        
            _hidingPlayer.Camera.SetPanAxisValue(panAxis);

            yield return null;
        }
        
        _hidingPlayer.transform.position = _unhidePosition.position;        
        _hidingPlayer.transform.rotation = _unhidePosition.rotation;
  
        _door.Close();
         
        _hidingPlayer.Camera.SetCameraInputEnabled(true);        
        _hidingPlayer.Movement.SetEnabled(true);        
        _hidingPlayer.InteractDetector.SetEnabled(true);        
        SetIsHiding(false); 
        _hidingPlayer = null;
         
        yield return new WaitWhile(() => _door.IsAnimating);
    }    
}
