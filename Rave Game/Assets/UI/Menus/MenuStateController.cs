using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuStateController : MonoBehaviour
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private List<MenuData> _menuDatas;
    private MenuStateChangedCommand _command = new MenuStateChangedCommand();

    private void Awake() 
    {
        _mediator.Subscribe<OpenMenuCommand<Leaf>>(OpenMenu);
        _mediator.Subscribe<OpenMenuCommand<Branch>>(OpenMenu);
        _mediator.Subscribe<CloseMenusCommand>(CloseMenu);
        _mediator.Subscribe<SetMenuInControllerComand>(SetMenu);
        CloseAllMenu();
    }

    public void CloseAllMenu()
    {
        CloseMenu(new CloseMenusCommand());
    }

    private void SetMenu(SetMenuInControllerComand callback)
    {
        foreach(MenuData menuData in _menuDatas)
            if(menuData.Transform == callback.Transform)
            {
                menuData.IDSettable = callback.IDSettable;
                menuData.IDSettable.SetID(menuData.ID);
            }   
    }

    private void OpenMenu<T>(OpenMenuCommand<T> callback) where T : IWithPosition
    {
        foreach(MenuData menuData in _menuDatas)
            if(menuData.ID == callback.ID)
            {
                _command.MenuState = true;
                _mediator.Publish(_command);
                ChangeMenuState(menuData, true, callback.Object.GetPosition());
            }
    }

    public void OpenMenu(string ID)
    {
        foreach(MenuData menuData in _menuDatas)
            if(menuData.ID == ID)
            {
                _command.MenuState = true;
                _mediator.Publish(_command);
                menuData.MenuBody.SetActive(true);
            } 
    }

    private void CloseMenu(CloseMenusCommand callback)
    {
        foreach(MenuData menuData in _menuDatas)
        {
            if(callback.IDs.Count == 0)
            {
                _command.MenuState = false;
                _mediator.Publish(_command);
                ChangeMenuState(menuData, false, Vector3.zero);
            }
            else
            {
                foreach(string identifier in callback.IDs)
                    if(menuData.ID == identifier)
                    {
                        _command.MenuState = false;
                        _mediator.Publish(_command);
                        ChangeMenuState(menuData, false, Vector3.zero);
                    }
            }
        }
    }

    private void ChangeMenuState(MenuData menuData, bool state, Vector3 objectPos)
    {
        if(state)
        {
            objectPos.y += menuData.YOffset;
            menuData.Transform.position = Camera.main.WorldToScreenPoint(objectPos);
        }
        menuData.MenuBody.SetActive(state);
    }
}


[Serializable]
public class MenuData
{
    [SerializeField] private string _id;
    [SerializeField] private Transform _transform;
    [SerializeField] private GameObject _menuBody;
    [SerializeField] private float _yOffset;
    public IIDSettable IDSettable;
    
    public string ID => _id;

    public Transform Transform => _transform;

    public GameObject MenuBody => _menuBody;

    public float YOffset => _yOffset;
}
