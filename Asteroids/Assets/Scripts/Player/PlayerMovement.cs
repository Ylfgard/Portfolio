using UnityEngine;

public class PlayerMovement
{
    private Transform _transform;
    private Transform _forwardPoint;
    private float _maxSpeed;
    private float _acceleration;
    private SendTransfrom _checkCrossing;

    private float _speed;
    private Vector2 _direction;
    

    public PlayerMovement(Transform transform, Transform forwardPoint, 
        float maxSpeed, float acceleration)
    {
        _transform = transform;
        _forwardPoint = forwardPoint;
        _maxSpeed = maxSpeed;
        _acceleration = acceleration;
        _checkCrossing = ScreenBorders.Instance.CheckBorderCrossing;
        _direction = Vector2.zero;
        _speed = 0;
    }

    public void Accelerate(float timeStep)
    {
        Vector2 acceleration = (_forwardPoint.position - _transform.position).normalized;
        acceleration *= _acceleration * timeStep;

        Vector2 moveVector = _direction * _speed;
        moveVector += acceleration;

        _direction = moveVector.normalized;
        _speed = moveVector.magnitude;
        if (_speed > _maxSpeed) _speed = _maxSpeed;
    }

    public void StopMoving()
    {
        _speed = 0;
        _direction = Vector2.zero;
    }

    public void Move(float timeStep)
    {
        Vector3 moveStep = _direction.normalized * _speed * timeStep;
        _transform.position += moveStep;
        _checkCrossing(_transform);
    }
}
