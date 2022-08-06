using UnityEngine;

public delegate void SendEvent();

public class PlayerMovement : MonoBehaviour
{
    private const float SpeedScale = 2;

    public event SendEvent OnPoint;

    [SerializeField]
    private PlayerAnimationController _animController;
    [SerializeField]
    private Transform _myTransform;
    [SerializeField]
    private Transform _pelvisBone;
    [SerializeField]
    private float _speed;

    private Vector3 _worldDir;
    private Vector3 _direction;
    private bool _moveToPoint;
    private Vector3 _movePoint;
    private float _distance;

    private void Start()
    {
        _direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (_moveToPoint && Vector3.Distance(_myTransform.position, _movePoint) < _distance)
        {
            Vector3 pos = (_myTransform.position - _movePoint).normalized * _distance + _movePoint;
            pos.y = 0;
            _myTransform.position = pos;
            OnPoint?.Invoke();
            _moveToPoint = false;
            _direction = Vector3.zero;
            _speed /= SpeedScale;
            return;
        }

        _myTransform.position += _direction.normalized * _speed * Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        if (_direction == Vector3.zero) return;
        Vector3 dir = _direction;
        if (_worldDir.z < 0) dir *= -1;
        Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.right);
        Vector3 rotation = _pelvisBone.eulerAngles;
        rotation.y = quaternion.eulerAngles.y - 90;
        _pelvisBone.eulerAngles = rotation;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = _myTransform.localToWorldMatrix * direction;
        _animController.PlayMoveAnim(direction);
        _worldDir = direction;
    }

    public void MoveToPoint(Vector3 point, float distance)
    {
        _movePoint = point;
        _distance = distance;
        _direction = point - _myTransform.position;
        _direction.y = 0;
        _animController.PlayMoveAnim(_direction);
        _worldDir = _direction;
        _moveToPoint = true;
        _speed *= SpeedScale;
    }
}