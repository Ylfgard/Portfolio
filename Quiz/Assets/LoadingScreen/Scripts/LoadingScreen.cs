using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Threading.Tasks;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingScreenImage; 
    [SerializeField] private float _fadeDurationLoadingScreen;
    [SerializeField] private UnityEvent _loadingScreenFadeOutComplete;
    [SerializeField] private UnityEvent _loadingScreenFadeInComplete;

    private void Start()
    {
        FadeOutLoadingScreen();
    }

    public async void FadeInLoadingScreen()
    {
        await FadeLoadingScreen(1);
        _loadingScreenFadeInComplete.Invoke();
    }

    public async void FadeOutLoadingScreen()
    {
        await FadeLoadingScreen(0);
        _loadingScreenFadeOutComplete.Invoke();
    }

    private async Task FadeLoadingScreen(float value)
    {
        await _loadingScreenImage.DOFade(value, _fadeDurationLoadingScreen).AsyncWaitForCompletion();
    }
}
