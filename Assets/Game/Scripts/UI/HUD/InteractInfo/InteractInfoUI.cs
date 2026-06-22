using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private RectTransform _uiRectTransform;    

    public void SetVisible(bool isVisible)
    {
        _uiObject?.SetActive(isVisible);
    }    
}
