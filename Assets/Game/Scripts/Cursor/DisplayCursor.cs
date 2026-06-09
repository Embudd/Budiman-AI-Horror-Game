using UnityEngine;

public class DisplayCursor : MonoBehaviour
{
    public static DisplayCursor Instance {get; private set;}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LockCursor()
    {
        Cursor.visible = false;        
        Cursor.lockState = CursorLockMode.Locked;            
    }

    public void UnlockCursor()
    {
        Cursor.visible = true;        
        Cursor.lockState = CursorLockMode.None;    
    }
}
