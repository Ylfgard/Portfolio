using UnityEngine;

public class PlayerWeaponSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject _sword;
    [SerializeField]
    private GameObject _mainWeapon;

    public void TakeSword()
    {
        _sword.SetActive(true);
        _mainWeapon.SetActive(false);
    }

    public void TakeMainWeapon()
    {
        _sword.SetActive(false);
        _mainWeapon.SetActive(true);
    }
}
