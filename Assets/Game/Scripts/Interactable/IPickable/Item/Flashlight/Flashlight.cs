using UnityEngine;

public class Flashlight : Item
{
    public override void Pickup(PlayerCharacter character)
    {
        _itemEvent.ItemPickUp(this);
        base.Pickup(character);
    }
}
