using UnityEngine;

public class Shooter
{
    private BulletSpawner _spawner;
    private ShooterType _type;
    private Transform _shootPoint;
    private Transform _directionPoint;
    private float _speed;

    public Shooter(ShooterType type, BulletSpawnerData data, 
        Transform shootPoint, Transform directionPoint, float speed)
    {
        CollisionObjectType bulletType;
        if (type == ShooterType.Player) bulletType = CollisionObjectType.PlayerBullet;
        else bulletType = CollisionObjectType.EnemyBullet;
        _spawner = new BulletSpawner(data, bulletType);
        
        _type = type;
        _shootPoint = shootPoint;
        _directionPoint = directionPoint;
        _speed = speed;
    }

    public Shooter(ShooterType type, BulletSpawner spawner,
        Transform shootPoint, Transform directionPoint, float speed)
    {
        _spawner = spawner;   
        _type = type;
        _shootPoint = shootPoint;
        _directionPoint = directionPoint;
        _speed = speed;
    }

    public void Shoot()
    {
        Vector2 dir;
        if (_type == ShooterType.Player) dir = _shootPoint.position - _directionPoint.position;
        else dir = _directionPoint.position - _shootPoint.position;
        _spawner.SpawnBullet(_shootPoint.position, dir, _speed);
    }
}

public enum ShooterType
{
    Player,
    UFO
}
