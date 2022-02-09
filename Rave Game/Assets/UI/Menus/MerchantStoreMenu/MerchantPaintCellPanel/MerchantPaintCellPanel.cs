using System;
using UnityEngine;
using UnityEngine.UI;
public class MerchantPaintCellPanel : PaintView
{
    [SerializeField]
    private Button _addPaintButton;
    [SerializeField]
    private Button _removePaintButton;
    [SerializeField]
    private NumberView _paintCellCountView;
    [SerializeField]
    private ColorView _paintColorView;
    private NumberCounter _paintCellCounter = new NumberCounter();
    public event Action ChangePaintCount;
    private void OnEnable()
    {
        _addPaintButton.onClick.AddListener(AddPaint);
        _removePaintButton.onClick.AddListener(RemovePaint);
        _addPaintButton.onClick.AddListener(UpdatePaintView);
        _removePaintButton.onClick.AddListener(UpdatePaintView);
    }
    private void OnDisable()
    {
        _addPaintButton.onClick.RemoveListener(AddPaint);
        _removePaintButton.onClick.RemoveListener(RemovePaint);
        _addPaintButton.onClick.RemoveListener(UpdatePaintView);
        _removePaintButton.onClick.RemoveListener(UpdatePaintView);
    }
    public void InitializePaint(int CombinationPrice, float priceIfAnimalIsNotAvaible)
    {
        _paint.Initialize(CombinationPrice, priceIfAnimalIsNotAvaible);
    }
    public void UsePaintCell()
    {
        Debug.Log(_paintCellCounter.Count);
        _paint.RemoveCount(_paintCellCounter.Count);
        _paintCellCounter.ReseCount();
        UpdatePaintView();
    }
    public int GetCount() => _paintCellCounter.Count;
    public override void UpdatePaintView()
    {
        _paintCellCountView.SetNumber(_paintCellCounter.Count);
        _paintColorView.SetColor(_paint.Color);
    }
    private void AddPaint()
    {
        if (_paintCellCounter.Count + 1 <= _paint.Count)
        {
            _paintCellCounter.AddNumber();
            ChangePaintCount?.Invoke();
        }

    }
    private void RemovePaint()
    {
        if (_paintCellCounter.Count - 1 >= 0)
        {
            _paintCellCounter.RemoveNumber();
            ChangePaintCount?.Invoke();
        }
    }
    
}