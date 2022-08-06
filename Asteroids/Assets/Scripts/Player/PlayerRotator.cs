using UnityEngine;

public class PlayerRotator
{
    private Transform _transform;
    private float _rotateSpeed;

    public PlayerRotator(Transform transform, float rotateSpeed)
    {
        _transform = transform;
        _rotateSpeed = rotateSpeed;
    }

    public void Rotate(float timeStep, TurnToSide side)
    {
        int rotateSide;
        if (side == TurnToSide.Right)
            rotateSide = -1;
        else
            rotateSide = 1;
        
        float rotateAngle = rotateSide * _rotateSpeed * timeStep;
        rotateAngle += _transform.localEulerAngles.z;
        Quaternion rotate = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
        _transform.rotation = rotate;
    }

    public void TurnToPoint(float timeStep, Vector2 position)
    {
        Vector3 targetDir = new Vector3(position.x, position.y, 0) - _transform.position;
        Quaternion targetRotate = Quaternion.LookRotation(targetDir, Vector3.forward);
        float rotateAngle = _rotateSpeed * timeStep;
        Quaternion rotate = Quaternion.RotateTowards(_transform.rotation, targetRotate, rotateAngle);
        rotate = Quaternion.Euler(0, 0, rotate.eulerAngles.z);
        _transform.rotation = rotate;
    }
}
