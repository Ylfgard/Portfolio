using UnityEngine;

public delegate void SendAsteroid(Asteroid asteroid, bool destroyed);

public class Asteroid : MonoBehaviour, ICollisionObject
{
    public SendAsteroid AsteroidCollided;
    public CollisionObjectType Type { get; set; } = CollisionObjectType.Asteroid;

    [SerializeField]
    private AsteroidSize _size;
    [SerializeField]
    private Transform _myTransform;

    private LinearMovement _mover;
    private int _pointsForAsteroid;
    private SendPoints _addPoints;
    
    public AsteroidSize Size => _size;
    public Vector2 Position => _myTransform.position;
    public Vector2 Direction => _mover.Direction;

    public void Initialize(Vector2 startPosition, Vector2 direction, float speed,
        SendPoints AddPoints, int pointsForAsteroid)
    {
        _myTransform.position = startPosition;
        _mover = new LinearMovement(_myTransform, direction, speed);
        _addPoints = AddPoints;
        _pointsForAsteroid = pointsForAsteroid;
    }

    public void ResetParametrs(Vector2 startPosition, Vector2 direction, float speed)
    {
        _myTransform.position = startPosition;
        _mover.ResetParamets(direction, speed);
    }

    private void FixedUpdate()
    {
        _mover.Move(Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collis = collision.gameObject.GetComponent<ICollisionObject>();
        if (collis == null) return;
        if (collis.Type == CollisionObjectType.Asteroid) return;

        _addPoints?.Invoke(_pointsForAsteroid);
        if (collis.Type == CollisionObjectType.Player ||
            collis.Type == CollisionObjectType.UFO)
            Destroying();
        else
            Splitting();
    }

    public void Destroying()
    {
        AsteroidCollided?.Invoke(this, true);
        gameObject.SetActive(false);
    }

    private void Splitting()
    {
        AsteroidCollided?.Invoke(this, false);
        gameObject.SetActive(false);
    }
}

public enum AsteroidSize
{
    Big,
    Medium,
    Small
}