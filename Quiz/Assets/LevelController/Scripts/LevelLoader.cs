using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private GameObject _card;
    [SerializeField] private float _cardSize;
    [SerializeField] private float _cardSpacing;
    [SerializeField] private Transform _generateZonePoint;
    private List<CardData> _levelCardsData;
    private List<Card> _cards = new List<Card>();

    public void LoadLevel()
    {
        _levelGenerator.GenerateLevelData(out _levelCardsData);
        SpawnCards();

        Vector3[] cardPositions = GenerateCardsPositions();
            
        for(int i = 0; i < _levelGenerator.Level.CardsCount; i++)
        {
            var cardData = _levelCardsData[i];
            Quaternion defaultRotate = Quaternion.Euler(0, 0, cardData.DefaultZRotate);
            _cards[i].LoadCardData(cardData.Identifier, cardData.Sprite, cardPositions[i], defaultRotate, _levelHandler);
        } 

        _levelCardsData.Clear();
    }

    private void SpawnCards()
    {
        if(_cards.Count < _levelGenerator.Level.CardsCount)
        {
            int newCardsCount = _levelGenerator.Level.CardsCount - _cards.Count;
            for(int i = 0; i < newCardsCount; i++)
            {
                Card card = Instantiate(_card, _generateZonePoint.position, Quaternion.identity).GetComponent<Card>();
                _cards.Add(card);
                card.transform.SetParent(_generateZonePoint);
            } 
        }
    }

    private Vector3[] GenerateCardsPositions()
    {
        float perCardOffset = _cardSize + _cardSpacing;
        float horizontalOffset = perCardOffset / 2 * _levelGenerator.Level.Columns - perCardOffset / 2;
        float verticalOffset = perCardOffset / 2 * _levelGenerator.Level.Lines - perCardOffset / 2;

        Vector3[] cardPositions = new Vector3[_levelGenerator.Level.CardsCount];
        int index = 0;
        for(float x = -horizontalOffset; x <= horizontalOffset; x += perCardOffset)
        {
            for(float y = -verticalOffset; y <= verticalOffset; y += perCardOffset)
            {
                cardPositions[index] = _generateZonePoint.position;
                cardPositions[index].x += x;
                cardPositions[index].y += y;
                index++;
            }
        }
        return cardPositions;
    }

    public void ClearCards()
    {
        foreach(Card card in _cards)
            card.DeleteCard();
        _cards.Clear();
    }

    public void TurnOffCardsInteractable()
    {
        foreach(Card card in _cards)
            card.TurnOffInteractable();
    }
}