using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private Image _crosshairImage;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _highlightColor;

    private void Awake()
    {
        SetHighligth(false);
    }

    public void SetHighligth(bool isSetHighlight)
    {
        if (isSetHighlight)
        {
            _crosshairImage.color = _highlightColor;
        }
        else
        {
            _crosshairImage.color = _normalColor;
        }
    }
}
