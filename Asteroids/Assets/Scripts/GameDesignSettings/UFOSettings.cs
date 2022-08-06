using UnityEngine;

public class UFOSettings : MonoBehaviour
{
    [Header("Balance Settings")]

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _minSpawnDelay;
    [SerializeField]
    private float _maxSpawnDelay;
    [SerializeField] [Range(1, 49)]
    private float _distanceFromBorders;
    [SerializeField]
    private float _minShootDelay;
    [SerializeField]
    private float _maxShootDelay;
    [SerializeField]
    private float _bulletSpeed;

    [Header("Points For Destruction")]

    [SerializeField]
    private int _UFO;

    [Header("Spawner Data")]

    [SerializeField]
    private UFOSpawner _spawner;
    [SerializeField]
    private BulletSpawnerData _bulletData;
    [SerializeField]
    private UFOSpawnerData _UFOData;

    private void Start()
    {
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        UFOBalanceSettings balance = new UFOBalanceSettings(_speed,
            _minShootDelay, _maxShootDelay, _bulletSpeed);
        
        _spawner.Initialize(_bulletData, _UFOData, balance, _minSpawnDelay,
            _maxSpawnDelay, _distanceFromBorders, _UFO);
    }
}

public struct UFOBalanceSettings
{
    public float Speed;
    public float MinShootDelay;
    public float MaxShootDelay;
    public float BulletSpeed;

    public UFOBalanceSettings(float speed, float minShootDelay,
        float maxShootDelay, float bulletSpeed)
    {
        Speed = speed;
        MinShootDelay = minShootDelay;
        MaxShootDelay = maxShootDelay;
        BulletSpeed = bulletSpeed;
    }
}