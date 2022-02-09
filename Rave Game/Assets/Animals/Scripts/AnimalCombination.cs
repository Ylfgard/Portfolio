using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "AnimalCombination", menuName = "Animal/Combination", order = 0)]
public class AnimalCombination : ScriptableObject
{
    [SerializeField] private List<Combination> _combinations;

    public List<Combination> Combinations => _combinations;

    public bool CheckCombination(List<Paint> paints)
    {
        paints.Sort(new PaintNameComparer());
        foreach(Combination combination in _combinations)
        {
            combination.Paints.Sort(new PaintNameComparer());
            if(combination.Paints.SequenceEqual(paints))
                return true;
        }
        return false;
    }
}
