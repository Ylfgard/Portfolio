using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour, ICollisionObject
{
    public event SendEvent Destroyed;
    public CollisionObjectType Type { get; set; } = CollisionObjectType.UFO;

    [SerializeField]
    private Transform _myTransform;

    private SendPoints _addPoints;
    private LinearMovement _mover;
    private Shooter _shooter;
    private float _speed;
    private float _minShootDelay;
    private float _maxShootDelay;
    private int _pointsForUFO;

    public void Initialize(UFOBalanceSettings balance, BulletSpawner bulletSpawner, Vector3 position,
        Vector2 direction, Transform playerTransform, SendPoints addPoints, int pointsForUFO)
    {
        _myTransform.position = position;
        _speed = balance.Speed;
        _mover = new LinearMovement(_myTransform, direction, _speed);
        _shooter = new Shooter(ShooterType.UFO, bulletSpawner, 
            _myTransform, playerTransform, balance.BulletSpeed);
        _minShootDelay = balance.MinShootDelay;
        _maxShootDelay = balance.MaxShootDelay;
        _addPoints = addPoints;
        _pointsForUFO = pointsForUFO;

        StartCoroutine(ShootCycle());
    }
    
    public void ResetParamets(Vector3 position, Vector2 direction)
    {
        _myTransform.position = position;
        _mover.ResetParamets(direction, _speed);

        StartCoroutine(ShootCycle());
    }

    private void FixedUpdate()
    {
        _mover.Move(Time.fixedDeltaTime);
    }

    private IEnumerator ShootCycle()
    {
        float delay = Random.Range(_minShootDelay, _maxShootDelay);
        yield return new WaitForSeconds(delay);
        Shoot();
        StartCoroutine(ShootCycle());
    }

    private void Shoot() => _shooter.Shoot();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collis = collision.gameObject.GetComponent<ICollisionObject>();
        if (collis == null) return;
        if (collis.Type == CollisionObjectType.UFO ||
            collis.Type == CollisionObjectType.EnemyBullet) return;

        Destroyed?.Invoke();
        _addPoints?.Invoke(_pointsForUFO);
        Destroying();
    }

    public void Destroying()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
