using UnityEngine;
using TMPro;

public class PlayerLives : MonoBehaviour
{
    public event SendEvent LivesWasted;

    [SerializeField]
    private TextMeshProUGUI _livesText;

    private int _startLives;
    private int _curLives;

    public void Initialize(int startLives)
    {
        _startLives = startLives;
        ResetLivesCount();
    }

    public bool LifeAvailable()
    {
        _curLives--;
        ShowLives();
        if (_curLives > 0)
        { 
            return true;
        }
        else
        {
            LivesWasted?.Invoke();
            return false;
        }
    }

    public void ResetLivesCount()
    {
        _curLives = _startLives;
        ShowLives();
    }

    private void ShowLives()
    {
        _livesText.text = "HP: ";
        for (int i = 0; i < _curLives; i++)
            _livesText.text += "|";
    }
}
