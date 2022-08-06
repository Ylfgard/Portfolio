using UnityEngine;
using TMPro;

public delegate void SendState(bool state);

public class PlayerFinishing : MonoBehaviour
{
    public SendState FinishingState;

    [SerializeField]
    private PlayerAnimationController _animationController;
    [SerializeField]
    private PlayerMovement _mover;
    [SerializeField]
    private PlayerTargetLook _targetLook;
    [SerializeField]
    private PlayerWeaponSwitcher _weaponSwitcher;
    [SerializeField]
    private GameObject _finishingText;
    [SerializeField]
    private float _distance;

    private Enemy _enemy;

    private void Start()
    {
        _mover.OnPoint += _animationController.PlayFinishingAnim;
        _mover.OnPoint += _weaponSwitcher.TakeSword;
    }

    public void ActiivateFinishing()
    {
        if (_enemy == null) return;
        FinishingState?.Invoke(true);
        _targetLook.LookAtTarget(_enemy.Position, 20);
        _mover.MoveToPoint(_enemy.Position, _distance);
    }

    public void KillEnemy()
    {
        _enemy.Death();
        _enemy = null;
        _finishingText.SetActive(false);
    }

    public void EndFinishing()
    {
        FinishingState?.Invoke(false);
        _weaponSwitcher.TakeMainWeapon();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy == null || enemy == _enemy) return;
        _enemy = enemy;
        _finishingText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != _enemy) return;
        _enemy = null;
        _finishingText.SetActive(false);
    }
}
