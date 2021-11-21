using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;

    private void Awake() 
    {
        HideRestartButton();
    }

    public void ShowRestartButton()
    {
        _restartButton.SetActive(true);
    }

    public void HideRestartButton()
    {
        _restartButton.SetActive(false);
    }
}
