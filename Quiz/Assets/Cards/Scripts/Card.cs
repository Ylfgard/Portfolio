using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _spripteTransform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private GameObject _starsParticles;
    private LevelHandler _levelHandler;
    private string _identifier;
    private bool _interactable;
    private Tween _answerTween;

    public void DeleteCard()
    {
        _answerTween?.Kill();
        Destroy(gameObject);
    }

    public void TurnOffInteractable()
    {
        _interactable = false;
    }

    public void LoadCardData(string identifier, Sprite sprite, Vector3 position, Quaternion defualtRotate, LevelHandler levelHandler)
    {
        _answerTween?.Kill();
        _identifier = identifier;
        _spriteRenderer.sprite = sprite;
        _levelHandler = levelHandler;
        _transform.position = position;
        _spripteTransform.rotation = defualtRotate;
        _spripteTransform.localScale = Vector3.one;
         _interactable = true;
    }

    private void OnMouseDown() 
    {
        if(_interactable)
        {
            _answerTween?.Kill();
            _spripteTransform.localPosition = Vector3.zero;
            if(_levelHandler.CheckCardIdentifier(_identifier))
            {
                Destroy(Instantiate(_starsParticles, _spripteTransform.position, Quaternion.identity), _tweenDuration);
                _answerTween = _spripteTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), _tweenDuration).SetEase(Ease.OutBounce);
            }
            else
            {
                _answerTween = _spripteTransform.DOShakePosition(_tweenDuration, 0.05f, 10, 90, false, false).SetEase(Ease.InBounce);
            }
        }
    }
}
