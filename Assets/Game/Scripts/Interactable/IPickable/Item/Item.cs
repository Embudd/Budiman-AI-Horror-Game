using System;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ItemData _data;

    public string Name => _data.Name;

    public event Action OnItemPicked;

    public void Interact()
    {
        Pickup();
    }

    public void Pickup()
    {
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}
