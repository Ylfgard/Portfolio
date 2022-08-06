using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ICollisionObject
{
    public CollisionObjectType Type { get; set; }

    [SerializeField]
    private Transform _myTransform;

    private LimitedLinearMovement _mover;

    public void Initialize(CollisionObjectType type, Vector3 position, Vector2 direction, float speed)
    {
        Type = type;
        _myTransform.position = position;
        float screenWidth = ScreenBorders.Instance.Borders.Right - ScreenBorders.Instance.Borders.Left;
        _mover = new LimitedLinearMovement(_myTransform, direction, speed, screenWidth);
        _mover.LimitOver += Destruction;
    }

    public void ResetParamets(Vector3 position, Vector2 direction, float speed)
    {
        _myTransform.position = position;
        _mover.ResetParamets(direction, speed);
    }

    private void FixedUpdate()
    {
        _mover.Move(Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collis = collision.gameObject.GetComponent<ICollisionObject>();
        if (collis == null) return;

        switch (Type)
        {
            case CollisionObjectType.PlayerBullet:
                if (collis.Type == CollisionObjectType.PlayerBullet ||
                    collis.Type == CollisionObjectType.Player) return;
                break;

            case CollisionObjectType.EnemyBullet:
                if (collis.Type == CollisionObjectType.EnemyBullet ||
                    collis.Type == CollisionObjectType.UFO) return;
                break;
        }

        Destruction();
    }

    public void Destruction()
    {
        gameObject.SetActive(false);
    }
}
