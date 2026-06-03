using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Can See Target", story: "Set [CanSeeTarget] from [AI]", category: "Action", id: "ecd2e2131e627c2fbd4dd6f474994c7e")]
public partial class SetCanSeeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanSeeTarget;
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

        CanSeeTarget.Value = AI.Value.SightPerception.CanSeeTarget;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

