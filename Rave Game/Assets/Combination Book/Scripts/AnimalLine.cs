using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Animals;

namespace CombinationBook
{
    public class AnimalLine : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private CombinationSpawner combinationSpawner;
        [SerializeField] private Image _animalImage;
        [SerializeField] private TextMeshProUGUI _animalName;
        [SerializeField] private Image _animalIncomeImg;
        [SerializeField] private Sprite _essenceSprite;
        [SerializeField] private TextMeshProUGUI _animalIncomeCount;

        public void Initialize(Transform parent, AnimalData data)
        {
            _transform.SetParent(parent);
            _animalImage.sprite = data.Behavior.BookSprite;
            _animalName.text = data.Name;
            string income = data.Behavior.Income.ToString() + " per\n";
            income += data.Behavior.Period.ToString() + " day(s)";
            _animalIncomeCount.text = income;
            if(data.Behavior.BringEssence)
                _animalIncomeImg.sprite = _essenceSprite;
            else
                _animalIncomeImg.color = data.Behavior.Paint.Color;
            combinationSpawner.SpawnCombinations(data.Combination.Combinations);
        }
    }
}
