using UnityEngine;

public class Key : Item
{
    public override void Pickup(PlayerCharacter character)
    {
        _itemEvent.ItemPickUp(this);
        base.Pickup(character);        
    }
}
