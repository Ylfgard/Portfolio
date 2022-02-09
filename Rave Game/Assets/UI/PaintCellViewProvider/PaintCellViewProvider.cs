using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PaintCellProvider<T> where T : PaintView
{
    [SerializeField]
    private T _paintViewPrefab;
    private List<PaintCell> _paints;
    [SerializeField]
    private Transform _parent;
    public List<T> Provide(Transform transform)
    {
        List<T> paintViews = new List<T>();
        Debug.Log(_paints.Count);
        for (int i = 0; i < _paints.Count; i++)
        {
            
            paintViews.Add(Object.Instantiate(_paintViewPrefab));
            paintViews[i].Initialize(_paints[i]);
            paintViews[i].SetParent(transform);
        }
        return paintViews;
    }
    public void SetPaints(List<PaintCell> paints)
    {
        _paints = paints;
    }
}