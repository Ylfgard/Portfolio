using UnityEngine;

public class PalettePaintView : PaintView
{
    [SerializeField]
    private ColorView _paintColorView;
    [SerializeField]
    private StringView _paintNameView;
    [SerializeField]
    private NumberView _paintCountView;
    public override void UpdatePaintView()
    {
        _paintNameView.SetString(_paint.Name);
        _paintCountView.SetNumber(_paint.Count);
        _paintColorView.SetColor(_paint.Color);
        ShowPanelIfPaintAvalible();
    }
}