using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public abstract class InteractableButton : MonoBehaviour
{
    private Button _interactButton;
    private void Awake()
    {
        _interactButton = GetComponent<Button>();
    }
    protected void OnEnable()
    {
        _interactButton.onClick.AddListener(Interact);
    }
    protected void OnDisable()
    {
        _interactButton.onClick.RemoveListener(Interact);
    }
    protected abstract void Interact();
}
