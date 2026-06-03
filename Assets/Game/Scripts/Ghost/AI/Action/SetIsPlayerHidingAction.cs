using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set IsPlayerHiding", story: "Set [IsPlayerHiding] from [AI]", category: "Action", id: "5f00f6865e81eee596c92a73ae94f6fa")]
public partial class SetIsPlayerHidingAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsPlayerHiding;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null || AI.Value.SightPerception == null)
        {
            return Status.Failure;
        }

        IsPlayerHiding.Value = !AI.Value.Target.IsHiding;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

