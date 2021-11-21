using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private CardDataKeeper _cardDataKeeper;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private UnityEvent _readyGenerateLevel;
    [SerializeField] private UnityEvent _allLevelsComplete;
    [SerializeField] private Level[] _levels;
    private int _currentLevelIndex = 0;
    private List<CardData> _choosedWinCards = new List<CardData>();

    public Level Level => _levels[_currentLevelIndex];

    public void ResetLevelIndex()
    {
        _currentLevelIndex = 0;
        _choosedWinCards.Clear();
    } 

    public void ChangeLevelIndexOnNext()
    {
        _currentLevelIndex++;
        if(_currentLevelIndex < _levels.Length)
            _readyGenerateLevel.Invoke();
        else
            _allLevelsComplete.Invoke();
    } 

    public void GenerateLevelData(out List<CardData> levelCardsData)
    {
        levelCardsData = new List<CardData>();
        List<CardData> randomCardsData = ChooseRandomCardsData();
        foreach(CardData cardData in randomCardsData)
            levelCardsData.Add(cardData);

        foreach(CardData choosedWinCard in _choosedWinCards)
            randomCardsData.Remove(choosedWinCard);
        int winCardIndex = Random.Range(0, randomCardsData.Count - 1);
        _choosedWinCards.Add(randomCardsData[winCardIndex]);
        _levelHandler.GiveWinCardIdentifier(randomCardsData[winCardIndex].Identifier);
    }

    private List<CardData> ChooseRandomCardsData()
    {
        List<CardData> cardsData = new List<CardData>();
        foreach(CardData cardData in _cardDataKeeper.CardsData)
            cardsData.Add(cardData);

        List<CardData> randomCardsData = new List<CardData>();
        for(int i = 0; i < _levels[_currentLevelIndex].CardsCount; i++)
        {
            int index = Random.Range(0, cardsData.Count);
            randomCardsData.Add(cardsData[index]);
            cardsData.Remove(cardsData[index]);
        }
        
        return randomCardsData;
    }
}

[System.Serializable]
public class Level
{
    [SerializeField] private int _columns;
    [SerializeField] private int _lines;

    public int Columns => _columns;
    
    public int Lines => _lines;
    
    public int CardsCount => _columns * _lines;
}