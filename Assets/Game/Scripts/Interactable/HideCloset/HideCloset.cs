using System.Collections;
using UnityEngine;

public class HideCloset : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;    

    [Header("Hide Config")]
    [SerializeField] private LeanTweenType _posEasingType;
    [SerializeField] private LeanTweenType _rotEasingType;    
    [SerializeField] private Transform _hidePosition;    
    [SerializeField] private Transform _unhidePosition;        
    [SerializeField] private float _duration = 1;            
    [SerializeField] private Door _door;    
             
    private PlayerCharacter _hidingPlayer;
    private Coroutine _hideCoroutine;
    private Coroutine _unhideCoroutine;
    private WaitWhile _waitWhileDoorAnimating;

    public bool IsHiding { get; private set; }
    public string Name => _name;

    private void Start()
    {
        _waitWhileDoorAnimating = new WaitWhile(() => _door.IsAnimating);
    }

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
    

    public IEnumerator Hide()
    {
        
        _hidingPlayer.SetIsHiding(true);        
        _hidingPlayer.Camera.SetCameraInputEnabled(false);        
        _hidingPlayer.Movement.SetEnabled(false);        
        _hidingPlayer.InteractDetector.SetEnabled(false);        
        _hidingPlayer.Camera.ResetCameraRotation();

        _hidingPlayer.transform.position = _unhidePosition.position;
         
        _door.Open();
 
        yield return _waitWhileDoorAnimating;
  
        Vector3 startPosition = _hidingPlayer.transform.position;        
        float startRotation = _hidingPlayer.Camera.PanAxis;     
        
        int moveID = LeanTween.move(_hidingPlayer.gameObject, _hidePosition.position, _duration).setEase(_posEasingType).id; 

        int rotateID = LeanTween.value(gameObject, startRotation, _hidePosition.eulerAngles.y, _duration)
                .setEase(_rotEasingType)
                .setOnUpdate((float val) => 
                {   
                    _hidingPlayer.Camera.SetPanAxisValue(val);
                }).id; 

        
        // Check if the tween finish
        while (true)
        {
            LTDescr move = LeanTween.descr(moveID);
            LTDescr rotate = LeanTween.descr(rotateID);

            if (move == null && rotate == null)
            {
                break;
            }

            yield return null;
        }

        _door.Close();
  
        yield return _waitWhileDoorAnimating;

        InputManager.Instance.OnInteractEvent += StopHiding;
    }

     public IEnumerator Unhide()
    {        
        InputManager.Instance.OnInteractEvent -= StopHiding;

        _door.Open();        
        yield return _waitWhileDoorAnimating;                 

        Vector3 startPosition = _hidingPlayer.transform.position;        
        float startRotation = _hidingPlayer.Camera.PanAxis;     

        int moveID = LeanTween.move(_hidingPlayer.gameObject, _unhidePosition.position, _duration).setEase(_posEasingType).id; 

        int rotateID = LeanTween.value(gameObject, startRotation, _hidePosition.eulerAngles.y, _duration)
                .setEase(_rotEasingType)
                .setOnUpdate((float val) => 
                {   
                    _hidingPlayer.Camera.SetPanAxisValue(val);
                }).id; 

         while (true)
        {
            LTDescr move = LeanTween.descr(moveID);
            LTDescr rotate = LeanTween.descr(rotateID);

            if (move == null && rotate == null)
            {
                break;
            }

            yield return null;
        }
  
        _door.Close();
         
        _hidingPlayer.Camera.SetCameraInputEnabled(true);        
        _hidingPlayer.Movement.SetEnabled(true);        
        _hidingPlayer.InteractDetector.SetEnabled(true);        
        _hidingPlayer.SetIsHiding(false); 
        _hidingPlayer = null;
         
        yield return _waitWhileDoorAnimating;
    }        
}
