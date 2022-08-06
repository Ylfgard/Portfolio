using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour, IScoreChanger
{
    public event SendPoints AddPoints;

    private AsteriodBalanceSettings _balanceSettings;
    private ObjectPool _bigAstrPool, _mediumAstrPool, _smallAstrPool;
    private int _curAsteroidCount;
    private int _pointsForBig;
    private int _pointsForMedium;
    private int _pointsForSmall;
    private GameStateController _gameController;
    private int _curStartCount;
    private bool _spawnDesabled;

    public void Initialize(AsteriodSpawnerData data, AsteriodBalanceSettings balanceSettings,
        int pointsForBig, int pointsForMedium, int pointsForSmall)
    {
        _balanceSettings = balanceSettings;
        _bigAstrPool = new ObjectPool(data.BigAsteroid, data.Container);
        _mediumAstrPool = new ObjectPool(data.MediumAsteroid, data.Container);
        _smallAstrPool = new ObjectPool(data.SmallAsteroid, data.Container);
        _pointsForBig = pointsForBig;
        _pointsForMedium = pointsForMedium;
        _pointsForSmall = pointsForSmall;
        _gameController = FindObjectOfType<GameStateController>();
        _curStartCount = balanceSettings.StartCount;
        _gameController.GameEnded += DeactivateSpawn;
        _gameController.GameStarted += ActivateSpawnStage;
    }

    private void ActivateSpawnStage()
    {
        _spawnDesabled = false;
        _curAsteroidCount = 0;
        for (int i = 0; i < _curStartCount; i++)
        {
            float speed = Random.Range(_balanceSettings.MinSpeed, _balanceSettings.MaxSpeed);
            Vector2 randomPoint = ScreenBorders.Instance.RandomPoint();
            SpawnAsteroid(AsteroidSize.Big, randomPoint, RandomDirection(), speed);
        }
        _curStartCount++;
    }

    private void DeactivateSpawn()
    {
        _spawnDesabled = true;
        _curStartCount = _balanceSettings.StartCount;
        StopAllCoroutines();
    }

    private Vector2 RandomDirection()
    {
        Vector2 dir;
        dir.x = Random.Range(-1f, 1f);
        dir.y = Random.Range(-1f, 1f);
        return dir;
    }

    private void SplitAsteroid(Asteroid asteroid, bool asteroidDestroyed)
    {
        asteroid.AsteroidCollided -= SplitAsteroid;
        if (_spawnDesabled) return;
        _curAsteroidCount--;
        if (asteroidDestroyed || asteroid.Size == AsteroidSize.Small)
        {
            if (_curAsteroidCount == 0)
                StartCoroutine(DelayedStageActivation());
            return;
        }

        AsteroidSize size = asteroid.Size;
        size++;

        float speed = Random.Range(_balanceSettings.MinSpeed, _balanceSettings.MaxSpeed);

        float angle = _balanceSettings.SplitAngle * Mathf.Deg2Rad;
        Vector2 curDir = asteroid.Direction;
        Vector2 dir1 = new Vector2(curDir.x * Mathf.Cos(angle) - curDir.y * Mathf.Sin(angle),
            curDir.y * Mathf.Cos(angle) + curDir.x * Mathf.Sin(angle));
        Vector2 dir2 = new Vector2(curDir.x * Mathf.Cos(-angle) - curDir.y * Mathf.Sin(-angle),
            curDir.y * Mathf.Cos(-angle) + curDir.x * Mathf.Sin(-angle));

        SpawnAsteroid(size, asteroid.Position, dir1, speed);
        SpawnAsteroid(size, asteroid.Position, dir2, speed);
    }

    private IEnumerator DelayedStageActivation()
    {
        yield return new WaitForSeconds(2);
        ActivateSpawnStage();
    }

    private void SpawnAsteroid(AsteroidSize size, Vector2 startPosition, Vector2 direction, float speed)
    {
        int pointsForAsteroid;
        ObjectPool pool = SelectPool(size, out pointsForAsteroid);
        
        bool objectIsNew = false;
        Asteroid asteroid = pool.GetObject(out objectIsNew).GetComponent<Asteroid>();
        if (objectIsNew)
        {
            asteroid.Initialize(startPosition, direction, speed, AddPoints, pointsForAsteroid);
            asteroid.AsteroidCollided += SplitAsteroid;
            _gameController.GameEnded += asteroid.Destroying;
        }
        else
        {
            asteroid.ResetParametrs(startPosition, direction, speed);
            asteroid.AsteroidCollided += SplitAsteroid;
        }
        _curAsteroidCount++;
    }

    private ObjectPool SelectPool(AsteroidSize size, out int pointsForAsteroid)
    {
        ObjectPool pool = _bigAstrPool;
        pointsForAsteroid = 0;
        switch (size)
        {
            case AsteroidSize.Big:
                pool = _bigAstrPool;
                pointsForAsteroid = _pointsForBig;
                break;

            case AsteroidSize.Medium:
                pool = _mediumAstrPool;
                pointsForAsteroid = _pointsForMedium;
                break;

            case AsteroidSize.Small:
                pool = _smallAstrPool;
                pointsForAsteroid = _pointsForSmall;
                break;
        }
        return pool;
    }
}