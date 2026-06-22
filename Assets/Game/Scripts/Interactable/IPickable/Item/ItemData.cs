using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string Name;
    public ItemID ID;

    public ItemData(string name, ItemID id) 
    {
        Name = name;
        ID = id;   
    }
}
