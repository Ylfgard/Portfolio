using UnityEngine;

public class PlayerTargetLook : MonoBehaviour
{
    [SerializeField]
    private Transform _targetBone;

    private Vector3 _target;
    private float _offset;

    public void LateUpdate()
    {
        Quaternion quaternion = Quaternion.LookRotation(_target - _targetBone.position, Vector3.up);
        Vector3 rotation = _targetBone.eulerAngles;
        rotation.y = quaternion.eulerAngles.y - 90 + _offset;
        _targetBone.eulerAngles = rotation;
    }

    public void LookAtTarget(Vector3 target)
    {
        _target = target;
        _offset = 0;
    }

    public void LookAtTarget(Vector3 target, float offset)
    {
        _target = target;
        _offset = offset;
    }
}
