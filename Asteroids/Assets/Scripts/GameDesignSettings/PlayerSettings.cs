using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Header("Balance Settings")]

    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _acceleration;
    [SerializeField] [Range(1, 360)]
    private float _rotateSpeedInDegree;
    [SerializeField]
    private float _shootDelay;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private int _livesCount;
    [SerializeField]
    private float _invulnerabilityTime;

    [Header("Player Data")]

    [SerializeField]
    private Player _player;
    [SerializeField]
    private PlayerLives _playerLives;
    [SerializeField]
    private BulletSpawnerData _data;

    private void Start()
    {
        InitializePlayer();
        _playerLives.Initialize(_livesCount);
    }

    private void InitializePlayer()
    {
        _player.Initialize(_data, _maxSpeed, _acceleration, _rotateSpeedInDegree,
            _shootDelay, _bulletSpeed, _playerLives, _invulnerabilityTime);
    }
}
