using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Despawn AI", story: "Despawn [AI]", category: "Action", id: "045ee1b22753be8e5fa9cc9474eaee4c")]
public partial class DespawnAiAction : Action
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

        AI.Value.Despawn();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

