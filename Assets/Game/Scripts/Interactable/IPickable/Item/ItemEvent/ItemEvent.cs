using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemEvent", menuName = "Scriptable Objects/ItemEvent")]
public class ItemEvent : ScriptableObject
{
    public event Action<Item> OnItemPicked;

    public void ItemPickUp(Item item)
    {
        OnItemPicked?.Invoke(item);
    }
}
