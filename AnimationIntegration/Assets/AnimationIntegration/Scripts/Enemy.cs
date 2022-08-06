using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform _myTransform;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private float _respwanTime;

    [Header ("Respawn Zone")]
    [SerializeField]
    private Vector2 _leftBackCorner;
    [SerializeField]
    private Vector2 _rightForwardCorner;

    public Vector3 Position => _myTransform.position;

    public void Death()
    {
        _anim.enabled = false;
        _collider.enabled = false;
        StartCoroutine(DelayedRespawn());
    }

    private IEnumerator DelayedRespawn()
    {
        yield return new WaitForSeconds(_respwanTime);
        float x = Random.Range(_leftBackCorner.x, _rightForwardCorner.x);
        float z = Random.Range(_leftBackCorner.y, _rightForwardCorner.y);
        Vector3 pos = new Vector3(x, 0, z);
        _myTransform.position = pos;
        _anim.enabled = true;
        _collider.enabled = true;
    }
}
