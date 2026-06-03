using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Agent Wait Until Reach Destination", story: "[AI] wait until reached destination", category: "Action", id: "f8d883cb716b12a8f2685081135cd593")]
public partial class AgentWaitUntilReachDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null)
        {
            return Status.Failure;
        }

        NavMeshAgent agent = AI.Value.NavMeshAgent;

        if (agent.pathPending)
        {
            return Status.Running;
        }

        float distanceThreshold = 1f; // This threshold must be same as behaviourgraph navigate to player
        if (agent.remainingDistance > agent.stoppingDistance + distanceThreshold)
        {
            return Status.Running;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

