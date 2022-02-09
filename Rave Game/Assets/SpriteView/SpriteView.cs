using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class SpriteView
{
    [SerializeField]
    private Image _spriteView;
    public void SetSprite(Sprite sprite)
    {
        _spriteView.sprite = sprite;
    }
}