using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalBehavior", menuName = "Animal/Behavior", order = 0)]
public class AnimalBehavior : ScriptableObject
{
    [SerializeField] private Sprite _bookSprite;
    [SerializeField] private Paint _unblokingPaint;
    [SerializeField] private Paint _paintIncome;
    [SerializeField] private bool _bringEssence;
    [SerializeField] private int _income;
    [SerializeField] private int _period;

    public Sprite BookSprite => _bookSprite;

    public Paint Paint => _paintIncome;

    public Paint UnblockingPaint => _unblokingPaint;

    public bool BringEssence => _bringEssence;

    public int Income => _income;

    public int Period => _period;
}
