using System.Collections.Generic;
using UnityEngine;

public class OpenMenuCommand<T> : ICommand where T : IWithPosition
{
    public string ID;
    public T Object;
}