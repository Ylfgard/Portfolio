using UnityEngine;

public delegate void SendEvent();

public class LimitedLinearMovement : LinearMovement
{
    public event SendEvent LimitOver;

    private float _distanceLimite;
    private float _path;

    public LimitedLinearMovement(Transform transform, Vector2 direction,
        float speed, float distanceLimite) : base(transform, direction, speed)
    {
        _checkCrossing = ScreenBorders.Instance.CheckBorderCrossing;
        _transform = transform;
        _distanceLimite = distanceLimite;
        ResetParamets(direction, speed);
    }

    public override void ResetParamets(Vector2 direction, float speed)
    { 
        base.ResetParamets(direction, speed);
        _path = 0;
    }

    public override void Move(float timeStep)
    {
        Vector3 moveStep = _direction.normalized * _speed * timeStep;
        _transform.position += moveStep;
        _checkCrossing(_transform);

        _path += moveStep.magnitude;
        if (_path >= _distanceLimite) LimitOver?.Invoke();
    }
}
