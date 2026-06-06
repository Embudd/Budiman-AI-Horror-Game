using UnityEngine;

public class DropObjectGameEvent : GameEventBase
{
    [SerializeField] private Rigidbody _dropObjectRb;

    public override void Trigger()
    {
        _dropObjectRb.useGravity = true;
        base.Trigger();
    }
}
