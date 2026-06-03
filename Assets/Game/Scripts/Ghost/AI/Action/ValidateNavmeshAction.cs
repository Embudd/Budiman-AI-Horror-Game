using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Validate Navmesh", story: "Validate navmesh from [AI]", category: "Action", id: "054e0b57c694e0fc85b9bc1e318a8ee8")]
public partial class ValidateNavmeshAction : Action
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

        if (AI.Value.NavMeshAgent == null ||AI.Value.NavMeshAgent.isActiveAndEnabled == false)
        {
            return Status.Failure;
        }

        if (AI.Value.NavMeshAgent.isOnNavMesh == false)
        {
            return Status.Failure;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

