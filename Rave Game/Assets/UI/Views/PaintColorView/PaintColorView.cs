using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ColorView
{
    [SerializeField]
    private Image _colorView;
    public void SetColor(Color color)
    {
        _colorView.color = color;
    }
}