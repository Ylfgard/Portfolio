using System;
using UnityEngine;
[Serializable]
public abstract class StoreItem : IBuyable, ICountable
{
    [SerializeField]
    private Sprite _itemSprite;
    public Sprite ItemSprite => _itemSprite;
    [SerializeField]
    protected int _price;
    [SerializeField]
    protected int _count;
    public int Count => _count;
    public virtual bool Buy(int money)
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
    public virtual int GetPrice()
    {
        return _price;
    }
    public void AddCount(int count)
    {
        if (count > 0)
            _count += count;
        else
            throw new InvalidOperationException();
    }
    public virtual void RemoveCount(int count)
    {
        if (count > 0 && _count >= count)
            _count -= count;
        else
            throw new InvalidOperationException();
    }
    public virtual bool HasEnoughMoneyToBuy(int money)
    {
        if (money >= _price)
            return true;
        else
            return false;
    }

}