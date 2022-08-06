using UnityEngine;

public class AsteroidsSettings : MonoBehaviour
{
    [Header ("Balance Settings")]

    [SerializeField]
    private int _startCount;
    [SerializeField]
    private float _minSpeed, _maxSpeed;
    [SerializeField] [Range(1, 89)]
    private int _splitAngle;

    [Header("Points For Destruction")]

    [SerializeField]
    private int _big;
    [SerializeField]
    private int _medium;
    [SerializeField]
    private int _small;

    [Header ("Spawner Data")]

    [SerializeField]
    private AsteroidsSpawner _spawner;
    [SerializeField]
    private AsteriodSpawnerData _data;


    private void Start()
    {
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        AsteriodBalanceSettings balanceSettings = new AsteriodBalanceSettings(
            _startCount, _minSpeed, _maxSpeed, _splitAngle);
        _spawner.Initialize(_data, balanceSettings, _big, _medium, _small);
    }
}