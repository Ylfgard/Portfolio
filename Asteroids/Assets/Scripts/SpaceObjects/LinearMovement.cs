using UnityEngine;

public delegate void SendTransfrom(Transform transform);

public class LinearMovement
{
    protected SendTransfrom _checkCrossing;
    protected Transform _transform;
    protected float _speed;
    protected Vector2 _direction;

    public Vector2 Direction => _direction;

    public LinearMovement(Transform transform, Vector2 direction, float speed)
    {
        _checkCrossing = ScreenBorders.Instance.CheckBorderCrossing;
        _transform = transform;
        ResetParamets(direction, speed);
    }

    public virtual void ResetParamets(Vector2 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    public virtual void Move(float timeStep)
    {
        Vector3 moveStep = _direction.normalized * _speed * timeStep;
        _transform.position += moveStep;
        _checkCrossing(_transform);
    }
}
