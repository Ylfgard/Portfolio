using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class ColoringMenu : MonoBehaviour, IIDSettable
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private Palette _palette;
    [SerializeField] private Image _colorOutput;
    [SerializeField] private TextMeshProUGUI _countOutput;
    [SerializeField] private int _coloringPrice;
    [SerializeField] private EventReference _leafSound;
    [SerializeField] private EventReference _coloringSound;
    private CloseMenusCommand _closeCommand = new CloseMenusCommand();
    private Leaf _leaf;
    private List<PaintCell> _paintCells;
    private int _currentPaintIndex;

    private void Awake()
    { 
        _mediator.Subscribe<OpenMenuCommand<Leaf>>(SetMenuData);   
        _mediator.Subscribe<PaintCellChangedCommand>(UpdateAvailablePaints);
    }

    private void Start()
    {
        SetMenuInControllerComand command = new SetMenuInControllerComand();
        command.IDSettable = this;
        command.Transform = this.gameObject.transform;
        _mediator.Publish(command); 
    }

    public void SetID(string id)
    {
        _closeCommand.IDs.Add(id);
    }

    private void UpdateAvailablePaints(PaintCellChangedCommand callback)
    {
        _paintCells = callback.PaintCells;
    }

    private void SetMenuData(OpenMenuCommand<Leaf> callback)
    {
        RuntimeManager.PlayOneShot(_leafSound);
        _currentPaintIndex = 0;
        _leaf = callback.Object;
        UpdateSelectedPaint();
    }

    public void CloseMenu()
    {
        _mediator.Publish(_closeCommand);
    }

    public void SelectPaint(bool next)
    {
        if(next)
        {
            _currentPaintIndex++;
            if(_currentPaintIndex >= _paintCells.Count)
                _currentPaintIndex = 0;
        }
        else
        {
            _currentPaintIndex--;
            if(_currentPaintIndex < 0)
                _currentPaintIndex = _paintCells.Count - 1;
        }
        UpdateSelectedPaint();
    }

    private void UpdateSelectedPaint()
    {
        var p = _paintCells[_currentPaintIndex];
        _colorOutput.color = p.Paint.Color;
        _countOutput.text = p.Count.ToString();
    }

    public void Colorize() 
    {
        if(_leaf.Colorized == false && _palette.ChangePaintCount(_paintCells[_currentPaintIndex], -_coloringPrice))
        {
            RuntimeManager.PlayOneShot(_coloringSound);
            _leaf.Colorize(_paintCells[_currentPaintIndex].Paint);
            UpdateSelectedPaint();
            CloseMenu();
        }
        else
        {
            CantColorize();
        }
    }

    private void CantColorize()
    {
        Debug.Log("Cant Colorize");
    }
}