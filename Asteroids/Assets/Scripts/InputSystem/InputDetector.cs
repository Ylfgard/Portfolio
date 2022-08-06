using UnityEngine;

public class InputDetector : MonoBehaviour
{
    [SerializeField]
    private Control _control;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private MenuFunctions _menu;

    private bool _inputActive;

    private void Update()
    {
        if (_inputActive == false) return;
        switch(_control)
        {
            case Control.Keyboard:
                if (Input.GetAxis("ForwardKeyboard") > 0)
                    _player.Accelerate(Time.deltaTime);

                if (Input.GetAxis("Horizontal") < 0)
                    _player.Rotate(Time.deltaTime, TurnToSide.Left);
                if (Input.GetAxis("Horizontal") > 0)
                    _player.Rotate(Time.deltaTime, TurnToSide.Right);

                if (Input.GetKeyDown(KeyCode.Space))
                    _player.Shoot();
                break;

            case Control.KeyboardPlusMouse:
                if (Input.GetAxis("ForwardKeyboard") > 0 || Input.GetAxis("Forward") > 0)
                    _player.Accelerate(Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                    _player.Shoot();

                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _player.TurnToPoint(Time.fixedDeltaTime, mousePos);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            _menu.ChangeMenuActive(true);
    }

    public Control ChangeControl()
    {
        switch(_control)
        {
            case Control.Keyboard:
                _control = Control.KeyboardPlusMouse;
                break;

            case Control.KeyboardPlusMouse:
                _control = Control.Keyboard;
                break;
        }
        return _control;
    }

    public void ChangeInputActive(bool state)
    {
        _inputActive = state;
    }
}

public enum Control
{
    Keyboard,
    KeyboardPlusMouse
}
