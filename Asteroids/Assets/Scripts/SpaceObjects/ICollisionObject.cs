public interface ICollisionObject
{
    public CollisionObjectType Type { get; set; }
}

public enum CollisionObjectType
{
    Player,
    Asteroid,
    UFO,
    EnemyBullet,
    PlayerBullet
}