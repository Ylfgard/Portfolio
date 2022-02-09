using System;
using UnityEngine;

[Serializable]
public class Desaturation : StoreItem
{
    public bool ChangeCount(int count)
    {
        if (_count + count >= 0)
        {
            _count += count;
            return true;
        }
        else
        {
            return false;
        }
    }
    public override bool Buy(int money)
    {
        if (money >= _price)
        {
            _count++;
            return true;
        }
        else
        {
            return false;
        }
    }
    public override int GetPrice()
    {
        return _price;
    }
}