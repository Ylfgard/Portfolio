using System.Collections.Generic;
using UnityEngine;
public class PaintMenu<T> : Menu where T : PaintView
{
    [SerializeField]
    protected Mediator _mediator;
    [SerializeField]
    protected List<PaintCell> _paints;
    [SerializeField]
    private PaintCellProvider<T> _paintPanelProvider;
    [SerializeField]
    protected List<T> _paintPanels;
    protected void Awake()
    {
        _mediator.Subscribe<PaintCellSendCommand>(RecivePaintCells);
    }
    protected void Start()
    {
        _paintPanelProvider.SetPaints(_paints);
        _paintPanels = _paintPanelProvider.Provide(_panelTransform);
        Shown += UpdatePaintPanels;
        UpdatePaintPanels();
    }
    private new void OnEnable()
    {
        base.OnEnable();
        Shown += UpdatePaintPanels;
    }
    private new void OnDisable()
    {
        base.OnDisable();
        Shown -= UpdatePaintPanels;
    }
    private void RecivePaintCells(PaintCellSendCommand callback)
    {
        _paints = callback.PaintCells;
    }
    private void UpdatePaintPanels()
    {
        foreach (T palettePaintView in _paintPanels)
        {
            palettePaintView.UpdatePaintView();
        }
    }
}