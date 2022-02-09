using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class CanAffortImage
{
    [SerializeField]
    private Image _canAffortImage;
    [SerializeField]
    private Sprite _canAffort;
    [SerializeField]
    private Sprite _cantAffort;
    public void ChangeAffortColor(int money, int price)
    {
        if (money >= price)
            _canAffortImage.sprite = _canAffort;
        else
            _canAffortImage.sprite = _cantAffort;
    }
}