using System;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ItemData _data;

    public string Name => _data.Name;

    public event Action OnItemPicked;

    [ContextMenu("Pickup Item")]
    public void Interact(PlayerCharacter character)
    {
        Pickup(character);
    }

    public virtual void Pickup(PlayerCharacter character)
    {
        ItemData item = new ItemData(_data.Name, _data.ID);
        character.Inventory.AddItem(item);

        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}
