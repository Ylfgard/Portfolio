using UnityEngine;
using TMPro;
using System.Linq;

public delegate void SendPoints(int points);

public class Score : MonoBehaviour
{
    public TextMeshProUGUI _scoreText;
    private int score;

    private void Start()
    {
        var scoreChangers = FindObjectsOfType<MonoBehaviour>().OfType<IScoreChanger>();
        foreach (var changer in scoreChangers)
            changer.AddPoints += ChangeScore;
        ResetScore();
        FindObjectOfType<GameStateController>().GameStarted += ResetScore;
    }

    private void ChangeScore(int points)
    {
        score += points;
        _scoreText.text = score.ToString();
    }

    private void ResetScore()
    {
        score = 0;
        _scoreText.text = score.ToString();
    }
}

public interface IScoreChanger
{
    public event SendPoints AddPoints;
}