using UnityEngine;
[RequireComponent(typeof(Panel))]
public abstract class PaintView : MonoBehaviour, IParentable
{
    protected PaintCell _paint;
    private Panel _paintViewPanel;
    private void Awake()
    {
        _paintViewPanel = GetComponent<Panel>();
    }
    public void Initialize(PaintCell paint)
    {
        _paint = paint;
    }
    public abstract void UpdatePaintView();
    public virtual void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
    protected void ShowPanelIfPaintAvalible()
    {
        if (_paint.Available)
            _paintViewPanel.Show();
        else
            _paintViewPanel.Hide();
    }
}