using System;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] protected ItemEvent _itemEvent;
    [SerializeField] private ItemData _data;
    public string Name => _data.Name;    

    public void Interact(PlayerCharacter character)
    {
        Pickup(character);
    }

    public virtual void Pickup(PlayerCharacter character)
    {
        ItemData item = new ItemData(_data.Name, _data.ID);
        character.Inventory.AddItem(item);

        Destroy(gameObject);
    }
}
