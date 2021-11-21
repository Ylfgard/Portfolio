using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointMover : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Transform _transform;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        _transform.position = playerTransform.position;
    }
}
