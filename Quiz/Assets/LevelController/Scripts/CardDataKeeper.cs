using UnityEngine;

public class CardDataKeeper : MonoBehaviour
{
    [SerializeField] private CardData[] _cardsData;

    public CardData[] CardsData => _cardsData;
}

[System.Serializable]
public class CardData
{
    [SerializeField] private string _identifier;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _defaultZRotate;
    
    public string Identifier => _identifier;
    
    public Sprite Sprite => _sprite;

    public float DefaultZRotate => _defaultZRotate;
}
