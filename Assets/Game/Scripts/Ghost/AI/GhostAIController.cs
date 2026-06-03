using UnityEngine;
using UnityEngine.AI;
using Unity.Behavior;
using System.Collections;

public class GhostAIController : MonoBehaviour
{
    public event System.Action OnGhostDespawn;

    [SerializeField] private SightPerception _sightPerception;
    [SerializeField] private PlayerCharacter _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private BehaviorGraphAgent _behaviorGraphAgent;    

    public SightPerception SightPerception => _sightPerception;
    public PlayerCharacter Target => _target;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public BehaviorGraphAgent BehaviorGraphAgent => _behaviorGraphAgent;

    public void Despawn()
    {
        StartCoroutine(DespawnAfterEndOfFrame());
    }

    private IEnumerator DespawnAfterEndOfFrame()
    {
        if (_behaviorGraphAgent != null)
        {
            _behaviorGraphAgent.SetVariableValue("CanSeeTarget", false);
            _behaviorGraphAgent.enabled = false;
        }

        if (_navMeshAgent != null && _navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.ResetPath();
            _navMeshAgent.enabled = false;
        }
        
        OnGhostDespawn?.Invoke();
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
