using UnityEngine;
using System.Collections;

public class UFOSpawner : MonoBehaviour, IScoreChanger
{
    public event SendPoints AddPoints;

    private ObjectPool _pool;
    private float _minSpawnDelay;
    private float _maxSpawnDelay;
    private Borders _spawnBorders;
    UFOBalanceSettings _balance;
    private BulletSpawner _bulletSpawner;
    private Transform _playerTransform;
    private int _pointsForUFO;
    private GameStateController _gameController;

    public void Initialize(BulletSpawnerData bulletData, UFOSpawnerData UFOData, UFOBalanceSettings balance, 
        float minSpawnDelay, float maxSpawnDelay, float distanceFromBorders, int pointsForUFO)
    {
        _minSpawnDelay = minSpawnDelay;
        _maxSpawnDelay = maxSpawnDelay;
        _balance = balance;
        _bulletSpawner = new BulletSpawner(bulletData, CollisionObjectType.EnemyBullet);
        _pool = new ObjectPool(UFOData.UFO, UFOData.UFOContainer);
        _spawnBorders = GetSpawnBorders(distanceFromBorders);
        _playerTransform = FindObjectOfType<Player>().Tranform;
        _pointsForUFO = pointsForUFO;
        _gameController = FindObjectOfType<GameStateController>();
        _gameController.GameStarted += ActivateSpawn;
        _gameController.GameEnded += DeactivateSpawn;
    }

    private Borders GetSpawnBorders(float distanceFromBorders)
    {
        Borders borders = ScreenBorders.Instance.Borders;
        float height = borders.Top - borders.Bottom;
        float offset = height * distanceFromBorders / 100;
        
        borders.Top += -offset;
        borders.Bottom += offset;

        return borders;
    }

    public void ActivateSpawn() => StartCoroutine(DelayedSpawn());

    public void DeactivateSpawn() => StopAllCoroutines();

    private IEnumerator DelayedSpawn()
    {
        float delay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        yield return new WaitForSeconds(delay);
        Spawn();
    }

    private void Spawn()
    {
        Vector2 direction;
        Vector3 position = GetSpawnPosition(out direction);
        bool ufoIsNew = false;
        UFO ufo = _pool.GetObject(out ufoIsNew).GetComponent<UFO>();
        
        if (ufoIsNew)
        {
            ufo.Initialize(_balance, _bulletSpawner, position,
                direction, _playerTransform, AddPoints, _pointsForUFO);
            _gameController.GameEnded += ufo.Destroying;
            ufo.Destroyed += ActivateSpawn;
        }
        else
        {
            ufo.ResetParamets(position, direction);
        }
    }
    
    private Vector3 GetSpawnPosition(out Vector2 direction)
    {
        Vector3 position = Vector3.zero;
        int side = Random.Range(0, 2);
        if (side == 1)
        {
            position.x = _spawnBorders.Left;
            direction = Vector2.right;
        }
        else
        {
            position.x = _spawnBorders.Right;
            direction = Vector2.left;
        }

        float spawnHeight = Random.Range(_spawnBorders.Bottom, _spawnBorders.Top);
        position.y = spawnHeight;
        return position;
    }
}
