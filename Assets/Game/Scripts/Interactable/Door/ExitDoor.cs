using System;
using UnityEngine;

public class ExitDoor : RotatingDoor
{
    public event Action<string> OnExitDoorOpened;

    public override void Open()
    {
        base.Open();
        
        DisplayCursor.Instance.UnlockCursor();
        OnExitDoorOpened?.Invoke("WinScreen");
    }
}
