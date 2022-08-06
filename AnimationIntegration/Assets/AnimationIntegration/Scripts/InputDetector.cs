using UnityEngine;

public class InputDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerFinishing _playerFinishing;
    [SerializeField]
    private PlayerTargetLook _targetLook;

    private bool _inputLocked;

    private void Start()
    {
        _playerFinishing.FinishingState += ChangeInputLock;
    }

    private void Update()
    {
        if (_inputLocked) return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x, 0, z);
        _playerMovement.SetDirection(dir);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        _targetLook.LookAtTarget(hit.point);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerFinishing.ActiivateFinishing();
    }

    private void ChangeInputLock(bool state)
    {
        _inputLocked = state;
    }
}
