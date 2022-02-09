using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Essence : StoreItem, ICountable
{
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
