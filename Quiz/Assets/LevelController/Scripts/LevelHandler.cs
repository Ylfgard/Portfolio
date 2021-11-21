using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private AnswerFader _answerFader;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private float _levelCompleteDelay; 
    [SerializeField] private UnityEvent _levelComplete;
    private string _winCardIdentifier;

    public void GiveWinCardIdentifier(string winCardIdentifier)
    {
        _winCardIdentifier = winCardIdentifier;
        _answerFader.FadeInAnswer(winCardIdentifier);
    }

    public bool CheckCardIdentifier(string identifier)
    {
        if(identifier == _winCardIdentifier)
        {
            StartCoroutine(LevelCompleteDelay());
            _levelLoader.TurnOffCardsInteractable();
           return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator LevelCompleteDelay()
    {
        yield return new WaitForSeconds(_levelCompleteDelay);
        _levelComplete.Invoke();
    }
}
