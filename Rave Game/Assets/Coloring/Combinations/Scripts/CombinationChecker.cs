using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animals;

public class CombinationChecker : MonoBehaviour
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private AnimalSpawner _animalSpawner;
    [SerializeField] private AnimalDataKeeper _animalDataKeeper;

    private void Start()
    {
        _mediator.Subscribe<CombinationIsAssembledCommand>(CheckCombination);
    }

    private void CheckCombination(CombinationIsAssembledCommand callback)
    {
        foreach(AnimalData animalData in _animalDataKeeper.AnimalsData)
            if(animalData.Combination.CheckCombination(callback.Paints))
                _animalSpawner.SendAnimalCommand(animalData, callback.AddCombination);
    }
}
