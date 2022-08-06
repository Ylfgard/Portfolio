using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public event SendEvent GameEnded;
    public event SendEvent GameStarted;

    private bool _gameActive;

    [SerializeField]
    private PlayerLives _playerLives;

    private void Start()
    {
        _gameActive = false;
        _playerLives.LivesWasted += EndGame;
    }

    public void StartNewGame()
    {
        if (_gameActive) GameEnded?.Invoke();
        GameStarted?.Invoke();
        _gameActive = true;
    }

    public void EndGame()
    {
        GameEnded?.Invoke();
        _gameActive = false;
    }
}