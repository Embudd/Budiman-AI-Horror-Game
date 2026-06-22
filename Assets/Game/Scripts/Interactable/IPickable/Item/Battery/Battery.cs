using UnityEngine;

public class Battery : Item
{    
    public override void Pickup(PlayerCharacter character)
    {
        _itemEvent.ItemPickUp(this);

        base.Pickup(character);
        character.Flashlight.RefillBatteryLevel();
    }
}
