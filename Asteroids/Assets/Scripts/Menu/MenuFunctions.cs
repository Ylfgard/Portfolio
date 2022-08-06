using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuBody;
    [SerializeField]
    private TextMeshProUGUI _controlText;
    [SerializeField]
    private InputDetector _input;
    [SerializeField]
    private GameStateController _gameController;
    [SerializeField]
    private Button _continueButton;

    private void Start()
    {
        ChangeControl();
        ChangeMenuActive(true);
        _continueButton.interactable = false;
        _gameController.GameEnded += DisableContinueButton;
        _gameController.GameEnded += OpenMenu;
    }

    private void OpenMenu()
    {
        ChangeMenuActive(true);
    }

    public void ChangeMenuActive(bool state)
    {
        if (state) Time.timeScale = 0;
        else Time.timeScale = 1;
        _menuBody.SetActive(state);
        bool inputState = !state;
        _input.ChangeInputActive(inputState);
    }

    public void StartNewGame()
    {
        _gameController.StartNewGame();
        _continueButton.interactable = true;
        ChangeMenuActive(false);
    }

    private void DisableContinueButton()
    {
        _continueButton.interactable = false;
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeControl()
    {
        Control control = _input.ChangeControl();
        if (control == Control.Keyboard)
            _controlText.text = "”правление: клавиатура";
        else
            _controlText.text = "”правление: клавиатура + мышь";
    }
}
