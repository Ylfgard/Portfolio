using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombinationBook
{
    public class CombinationSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _combinationPref;
        [SerializeField] private Transform _combinationsParent;

        public void SpawnCombinations(List<Combination> combinations)
        {
            foreach(Combination combination in combinations)
            {
                CombinationPaintSpawner spawner = Instantiate(_combinationPref, _combinationsParent.position,
                 Quaternion.identity).GetComponent<CombinationPaintSpawner>();
                spawner.Initialize(_combinationsParent, combination);
            }
        }
    }
}
