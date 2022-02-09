using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combination", menuName = "Coloring/Combination", order = 0)]
public class Combination : ScriptableObject 
{
    [SerializeField] private List<Paint> _paints;

    public List<Paint> Paints => _paints;
}
