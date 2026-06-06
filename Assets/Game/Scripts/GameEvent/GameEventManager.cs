using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }

    private Dictionary<string, GameEventBase> _gameEvents = new Dictionary<string, GameEventBase>();

    private void Awake()
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

    public void RegisterEvent(GameEventBase gameEvent)
    {
        if (!_gameEvents.ContainsKey(gameEvent.ID))
        {
            _gameEvents.Add(gameEvent.ID, gameEvent);
        }
    }

    public void UnregisterEvent(GameEventBase gameEvent)
    {
        if (_gameEvents.ContainsKey(gameEvent.ID))
        {
            _gameEvents.Remove(gameEvent.ID);
        }
    }

    public void TriggerEvent(string eventID)
    {
        if (_gameEvents.TryGetValue(eventID, out GameEventBase gameEvent))
        {
            gameEvent.Trigger();
        }
    }

    public void FinishEvent(string eventID)
    {
        if (_gameEvents.TryGetValue(eventID, out GameEventBase gameEvent))
        {
            gameEvent.Finish();
        }
    }    
}
