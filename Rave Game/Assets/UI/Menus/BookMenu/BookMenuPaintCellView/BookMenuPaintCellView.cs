using UnityEngine;
public class BookMenuPaintCellView : PaintView
{
    [SerializeField]
    private ColorView _paintColorView;
    [SerializeField]
    private StringView _paintNameView;
    public override void UpdatePaintView()
    {
        _paintColorView.SetColor(_paint.Color);
        _paintNameView.SetString(_paint.Name);
    }
}