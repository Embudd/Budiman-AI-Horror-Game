using UnityEngine;
using UnityEngine.AI;
using Unity.Behavior;

public class GhostAIController : MonoBehaviour
{
    [SerializeField] private SightPerception _sightPerception;
    [SerializeField] private PlayerCharacter _target;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private BehaviorGraphAgent _behaviorGraphAgent;    

    public SightPerception SightPerception => _sightPerception;
    public PlayerCharacter Target => _target;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public BehaviorGraphAgent BehaviorGraphAgent => _behaviorGraphAgent;
}
