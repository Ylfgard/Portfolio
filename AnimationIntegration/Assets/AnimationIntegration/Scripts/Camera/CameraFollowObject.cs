using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform _myTransfrom;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector3 _position;

    private void Start()
    {
        _position = _target.localToWorldMatrix * _position;
    }

    private void LateUpdate()
    {
        _myTransfrom.position = _target.position + _position;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = _target.localToWorldMatrix * _position;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_target.position + pos, 0.1f);
    }
}
