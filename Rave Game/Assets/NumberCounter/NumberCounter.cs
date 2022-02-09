using System;
public class NumberCounter
{
    private int _count;
    public int Count => _count;
    public event Action<int> NumberChange;
    public void AddNumber()
    {
        _count++;
        NumberChange?.Invoke(_count);
    }
    public void RemoveNumber()
    {
        _count--;
        NumberChange?.Invoke(_count);
    }
    public void ReseCount()
    {
        _count = 0;
        NumberChange?.Invoke(_count);
    }
}