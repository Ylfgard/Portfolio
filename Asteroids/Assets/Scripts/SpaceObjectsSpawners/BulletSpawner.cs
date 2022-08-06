using UnityEngine;

public class BulletSpawner
{
    private ObjectPool _bulletPool;
    private CollisionObjectType _bulletType;
    private GameStateController _gameController;

    public BulletSpawner(BulletSpawnerData data, CollisionObjectType bulletType)
    {
        _bulletPool = new ObjectPool(data.Bullet, data.BulletContainer);
        _bulletType = bulletType;
        _gameController = MonoBehaviour.FindObjectOfType<GameStateController>();
    }

    public void SpawnBullet(Vector3 position, Vector2 direction, float speed)
    {
        bool bulletIsNew = false;
        Bullet bullet = _bulletPool.GetObject(out bulletIsNew).GetComponent<Bullet>();
        if (bulletIsNew)
        {
            bullet.Initialize(_bulletType, position, direction, speed);
            _gameController.GameEnded += bullet.Destruction;
        }
        else
        {
            bullet.ResetParamets(position, direction, speed);
        }
    }
}
