using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionMover : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerMovement playerMovement;
    private Transform _transform;

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(playerMovement.xDirection == 0 && playerJump.xDirection == 0)
            _transform.position = Vector3.Lerp(playerTransform.position, mousePosition, 0.3f);
        else
            _transform.position = playerTransform.position;
    }
}
