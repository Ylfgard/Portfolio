using UnityEngine;
using TMPro;
using DG.Tweening;

public class AnswerFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _questTextGroup; 
    [SerializeField] private TextMeshProUGUI _answerText;
    [SerializeField] private float _fadeDuration;

    private void Start() 
    {
        FadeOutAnswer();
    }

    public void FadeInAnswer(string answer)
    {
        _answerText.text = answer;
        FadeText(1);
    }

    public void FadeOutAnswer()
    {
        FadeText(0);
    }
    
    private void FadeText(float value)
    {
        _questTextGroup.DOFade(value, _fadeDuration);
    }
}
