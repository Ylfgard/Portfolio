using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    public class AnimalSpawner : MonoBehaviour
    {
        [SerializeField] private Mediator _mediator;
        [SerializeField] private Palette _palette;
        [SerializeField] private FinishDayButton _finishDayButton;
        private List<AnimaNextDayCommand> _nextDayCommands = new List<AnimaNextDayCommand>();

        private void Start() 
        {
            _finishDayButton.Clicked.AddListener(ComplitingNextDayCommands);
            _nextDayCommands.Clear();
        }

        private void SpawnAnimal(AnimalData animalData)
        {
            if(animalData.ExistingAnimals.Count == 0)
                ShowAnimal(animalData);
            Animal animal = Instantiate(animalData.Prefab, Vector3.zero, Quaternion.identity).GetComponent<Animal>();
            animal.Init(_palette, _mediator);
            _finishDayButton.Clicked.AddListener(animal.EndDay);
            AddAnimalCombinationCommand command = new AddAnimalCombinationCommand();
            command.AnimalData = animalData;
            _mediator.Publish(command);
            animalData.AddAnimal(animal);
        }

        public void SendAnimalCommand(AnimalData animalData, bool toSpawn)
        {
            AnimaNextDayCommand command = new AnimaNextDayCommand(animalData, toSpawn);
            AddCommand(command);
        }

        private void ComplitingNextDayCommands()
        {
            foreach(AnimaNextDayCommand nextDayCommand in _nextDayCommands)
            {
                if(nextDayCommand.ToSpawn) SpawnAnimal(nextDayCommand.AnimalData);
                else DespawnAnimal(nextDayCommand.AnimalData);
            }
            _nextDayCommands.Clear();
        }

        private void AddCommand(AnimaNextDayCommand command)
        {
            foreach(AnimaNextDayCommand nextDayCommand in _nextDayCommands)
            {
                if(nextDayCommand.AnimalData == command.AnimalData && nextDayCommand.ToSpawn != command.ToSpawn)
                {
                    _nextDayCommands.Remove(nextDayCommand);
                    Debug.Log("Remove animal command");
                    return;
                }
            }
            _nextDayCommands.Add(command);
            Debug.Log("Add animal");
        }

        private void DespawnAnimal(AnimalData animalData)
        {
            if(animalData.ExistingAnimals.Count == 0)
            {
                Debug.LogError("Try delete not existing animal!");
                return;
            }
            else
            {
                _finishDayButton.Clicked.RemoveListener(animalData.ExistingAnimals[0].EndDay);
                animalData.RemoveAnimal(animalData.ExistingAnimals[0]);
                if(animalData.ExistingAnimals.Count == 0)
                    HideAnimal(animalData);
            }
        }

        private void ShowAnimal(AnimalData animalData)
        {
            if(animalData.AnimalOnScene != null)
                animalData.AnimalOnScene.SetActive(true);
        }

        private void HideAnimal(AnimalData animalData)
        {
            if(animalData.AnimalOnScene != null)
                animalData.AnimalOnScene.SetActive(false);
        }
    }

    public class AnimaNextDayCommand
    {
        private AnimalData _animalData;
        private bool _toSpawn;

        public AnimalData AnimalData => _animalData;

        public bool ToSpawn => _toSpawn;

        public AnimaNextDayCommand(AnimalData animalData, bool toSpawn)
        {
            _animalData = animalData;
            _toSpawn = toSpawn;
        }
    }
}

