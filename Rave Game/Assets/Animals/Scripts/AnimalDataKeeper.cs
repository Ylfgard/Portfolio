using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Animals
{
    public class AnimalDataKeeper : MonoBehaviour
    {
        [SerializeField] private Mediator _mediator;
        [SerializeField] private List<AnimalData> _animalsData;
        private List<string> _addedAnimalsCombinationsName = new List<string>();
        
        public List<AnimalData> AnimalsData => _animalsData;
        
        private void Awake()
        {
            _mediator.Subscribe<AddedAnimalCombinationsCommand>(AnimalAddedInBook);
            foreach(AnimalData animalData in _animalsData)
            {
                if(animalData.AnimalOnScene != null)
                    animalData.AnimalOnScene.SetActive(false);
            }    
        }
        public bool UnblockCombination(PaintCell paintCell)
        {
            foreach(AnimalData data in _animalsData)
            {
                bool notAdded = true;
                foreach(string name in _addedAnimalsCombinationsName)
                {
                    if(data.Name == name)
                    {
                        notAdded = false;
                        break;
                    }
                }
                if(notAdded && data.Behavior.UnblockingPaint == paintCell.Paint)
                {
                    AddAnimalCombinationCommand command = new AddAnimalCombinationCommand();
                    command.AnimalData = data;
                    _mediator.Publish(command);
                    return true;
                }
            }
            return false;
        }

        private void AnimalAddedInBook(AddedAnimalCombinationsCommand callback)
        {
            _addedAnimalsCombinationsName = callback.AnimalsNames;
        }
    }

    [Serializable]
    public class AnimalData
    {
        [SerializeField] private string _name;
        [SerializeField] private AnimalCombination _combination;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private AnimalBehavior _behavior;
        [SerializeField] private GameObject _animalOnLevel;
        private List<Animal> _existingAnimals = new List<Animal>();

        public string Name => _name;

        public AnimalCombination Combination => _combination;

        public GameObject Prefab => _prefab;
        
        public AnimalBehavior Behavior => _behavior;

        public GameObject AnimalOnScene => _animalOnLevel;

        public List<Animal> ExistingAnimals => _existingAnimals;

        public void AddAnimal(Animal animal) 
        {
            _existingAnimals.Add(animal);
        }

        public void RemoveAnimal(Animal animal)
        {
            _existingAnimals.Remove(animal);
            animal.Despawn();
        }
    }
}


    
