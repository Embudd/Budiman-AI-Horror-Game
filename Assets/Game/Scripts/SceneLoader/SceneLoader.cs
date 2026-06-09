using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _character;
    [SerializeField] private ExitDoor _door;

    void Start()
    {    
        if (_character != null)
        {
            _character.OnPlayerDeath += LoadScene;    
        }
        
        if (_door != null)
        {
            _door.OnExitDoorOpened += LoadScene; 
        }        
    }

    void OnDestroy()
    {
        if (_character != null)
        {
            _character.OnPlayerDeath -= LoadScene;    
        }
        
        if (_door != null)
        {
            _door.OnExitDoorOpened -= LoadScene;
        }        
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
