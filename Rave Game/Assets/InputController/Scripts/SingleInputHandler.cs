using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInputHandler : MonoBehaviour
{
    [SerializeField] private Mediator _mediator;
    private CloseMenusCommand _closeMenusCommand = new CloseMenusCommand();
    private bool _menuOpen;
    private Vector2 _lastInputPosition;

    private void Awake() 
    {
        _mediator.Subscribe<MenuStateChangedCommand>(MenuStateChanged);
    }

    public void InputDown(Vector2 touchPos)
    {
        if(_menuOpen == false)
        {
            _lastInputPosition = touchPos;
            RaycastHit2D ray = Physics2D.Raycast(touchPos, Vector2.one * 0.0001f);
            Collider2D c2d = ray.collider;
            if (c2d != null)
            {
                _mediator.Publish(_closeMenusCommand);
                switch(c2d.gameObject.tag)
                {
                    case "Leaf": 
                        SendOpenMenuCommand<Leaf>(c2d.GetComponent<Leaf>(), "Leaf");
                        return;
                    
                    case "Branch":
                        SendOpenMenuCommand<Branch>(c2d.GetComponent<Branch>(), "Branch");
                        return;

                    default:
                        return;
                }   
            }
        }
    }

    private void MenuStateChanged(MenuStateChangedCommand callback)
    {
        _menuOpen = callback.MenuState;
    }

    private void SendOpenMenuCommand<T>(T choosedObj, string id) where T : IWithPosition
    {
        OpenMenuCommand<T> openMenuCommand = new OpenMenuCommand<T>();
        openMenuCommand.Object = choosedObj;
        openMenuCommand.ID = id;
        _mediator.Publish(openMenuCommand);
    }

    public void InputPress(Vector2 touchPos)
    {

    }

    public void InputMove(Vector2 touchPos)
    {
        //
        // ...Do some stuff
        //
        _lastInputPosition = touchPos;
    }

    public void InputUp(Vector2 touchPos)
    {

    }
}
