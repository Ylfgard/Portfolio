using UnityEngine;
using System;

[Serializable]
public struct AsteriodSpawnerData
{
    public Transform Container;

    [Header ("Prefabs")]

    public GameObject BigAsteroid;
    public GameObject MediumAsteroid;
    public GameObject SmallAsteroid;
}

public struct AsteriodBalanceSettings
{
    public int StartCount;
    public float MinSpeed, MaxSpeed;
    public int SplitAngle;

    public AsteriodBalanceSettings(int startCount, float minSpeed, float maxSpeed, int splitAngle)
    {
        StartCount = startCount;
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
        SplitAngle = splitAngle;
    }
}