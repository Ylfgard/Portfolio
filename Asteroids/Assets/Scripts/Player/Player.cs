using UnityEngine;
using System.Collections;
using UnityEngine.U2D;

public class Player : MonoBehaviour, ICollisionObject
{
    private const float RespawnDelay = 2;
    private const float AccelerationSoundDelay = 0.2f;

    public CollisionObjectType Type { get; set; } = CollisionObjectType.Player;

    [Header("Player Data")]
    [SerializeField]
    private Transform _myTransform;
    [SerializeField]
    private Collider2D _myCollider;
    [SerializeField]
    private SpriteShapeRenderer _myRenderer;
    [SerializeField]
    private Transform _shootPoint;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _audioSource;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip _acceleration;
    [SerializeField]
    private AudioClip _shoot;
    [SerializeField]
    private AudioClip _explosion;

    private float _shootDelay;
    private bool _shotReady;
    private PlayerMovement _mover;
    private PlayerRotator _rotator;
    private Shooter _shooter;
    private PlayerLives _lives;
    private bool _isDead;
    private float _invulnerabilityDuration;
    private bool _readyPlaySound = true;

    public Transform Tranform => _myTransform;

    public void Initialize(BulletSpawnerData data, float maxSpeed, float acceleration, float rotateSpeed,
        float shootDelay, float bulletSpeed, PlayerLives lives, float invulnerabilityDuration)
    {
        _mover = new PlayerMovement(_myTransform, _shootPoint, maxSpeed, acceleration);
        _rotator = new PlayerRotator(_myTransform, rotateSpeed);
        _shooter = new Shooter(ShooterType.Player, data, _shootPoint, _myTransform, bulletSpeed);
        _shootDelay = shootDelay;
        _shotReady = true;
        _lives = lives;
        _isDead = false;
        _invulnerabilityDuration = invulnerabilityDuration;
        FindObjectOfType<GameStateController>().GameStarted += Restart;
    }

    private void FixedUpdate()
    {
        if (_isDead) return;
        _mover.Move(Time.fixedDeltaTime);
    }

    public void Shoot()
    {
        if (_isDead || _shotReady == false) return; 
        _shooter.Shoot();
        _audioSource.PlayOneShot(_shoot);
        _shotReady = false;
        Invoke("ShootReload", _shootDelay);
    }
    
    private void ShootReload() => _shotReady = true;
    
    public void Accelerate(float timeStep)
    {
        if (_isDead) return;
        PlaySoundWithDelay(_acceleration, _acceleration.length - 0.05f);
        _mover.Accelerate(timeStep);
    }

    private void PlaySoundWithDelay(AudioClip sound, float delay)
    {
        if (_readyPlaySound == false) return; 
        _audioSource.PlayOneShot(sound);
        _readyPlaySound = false;
        Invoke("AllowPlaySound", delay);
    }

    private void AllowPlaySound()
    {
        _readyPlaySound = true;
    }
    
    public void TurnToPoint(float timeStep, Vector2 position)
    {
        if (_isDead) return;
        _rotator.TurnToPoint(timeStep, position);
    }
    

    public void Rotate(float timeStep, TurnToSide side)
    {
        if (_isDead) return;
        _rotator.Rotate(timeStep, side);
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collis = collision.gameObject.GetComponent<ICollisionObject>();
        if (collis == null) return;
        if (collis.Type == CollisionObjectType.PlayerBullet) return;

        Destruction();
    }

    private void Destruction()
    {
        _audioSource.PlayOneShot(_explosion); 
        ChangePlayerActive(false);
        if (_lives.LifeAvailable())
            StartCoroutine(DelayedRespawn());
    }
    
    private IEnumerator DelayedRespawn()
    {
        yield return new WaitForSeconds(RespawnDelay);
        ChangePlayerActive(true);
    }

    private void Restart()
    {
        ChangePlayerActive(true);
        _lives.ResetLivesCount();
    }

    private void ChangePlayerActive(bool state)
    {
        StopAllCoroutines();
        _mover.StopMoving();
        if (state == false)
        {
            _myCollider.enabled = state;
        }
        else
        {
            Vector3 spawnPos = Camera.main.transform.position;
            spawnPos.z = 0;
            _myTransform.position = spawnPos;
            StartCoroutine(ActivateInvulnerability());
        }
        _myRenderer.enabled = state;
        _isDead = !state;
    }

    private IEnumerator ActivateInvulnerability()
    {
        _myCollider.enabled = false;
        _animator.SetBool("Invulnerable", true);
        yield return new WaitForSeconds(_invulnerabilityDuration);
        _animator.SetBool("Invulnerable", false);
        _myCollider.enabled = true;
    }

}

public enum TurnToSide
{
    Right,
    Left
}