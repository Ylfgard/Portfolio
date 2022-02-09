using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animals;

namespace CombinationBook
{
    public class Page : MonoBehaviour
    {
        [SerializeField] private GameObject _animalLine;
        [SerializeField] private Transform _animalLineParent;
        [SerializeField] private Transform _transform;

        public void Initialize(Transform parent)
        {
            _transform.SetParent(parent);
        }

        public void SpawnAnimalLine(AnimalData data)
        {
            AnimalLine line = Instantiate(_animalLine, _animalLineParent.position, Quaternion.identity).GetComponent<AnimalLine>();
            line.Initialize(_animalLineParent, data);
        }
    }
}

