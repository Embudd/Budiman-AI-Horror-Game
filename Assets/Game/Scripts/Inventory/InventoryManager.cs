using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private List<ItemData> _items = new List<ItemData>();
    public List<ItemData> Items => _items;

    public void AddItem(ItemData item)
    {
        Items.Add(item);
    }

    public bool CheckItem(ItemID keyID)
    {
        bool isExists = Items.Exists(ItemData => string.Equals(ItemData.ID, keyID));
        return isExists;
    }

    public void RemoveItem(ItemData item)
    {
        Items.Remove(item);
    }    
}
