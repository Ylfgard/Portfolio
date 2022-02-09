using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationIsAssembledCommand : ICommand
{
    public List<Paint> Paints = new List<Paint>();
    public bool AddCombination;
}
