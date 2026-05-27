using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string Name;
    public string ID;

    public ItemData(string name, string id) 
    {
        Name = name;
        ID = id;   
    }
}
