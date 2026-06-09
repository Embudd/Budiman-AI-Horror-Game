using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private RectTransform _uiRectTransform;
    [SerializeField] private TextMeshProUGUI _itemNameText;

    private StringBuilder _itemNameSB = new StringBuilder();

    public void SetVisible(bool isVisible)
    {
        _uiObject?.SetActive(isVisible);
    }

    public void SetItemNameText(string itemName)
    {
        _itemNameSB.Clear();

        _itemNameSB.Append(itemName);
        _itemNameText.text = _itemNameSB.ToString();

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_uiRectTransform);
        Canvas.ForceUpdateCanvases();
    }
}
