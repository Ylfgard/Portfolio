using UnityEngine;
using UnityEngine.UI;
using System;
public class Menu : MonoBehaviour
{
    public event Action Shown;
    public event Action Hid;
    [SerializeField]
    private Button _showButton;
    [SerializeField]
    private Button _hideButton;
    [SerializeField]
    protected Panel _menuPanel;
    [SerializeField]
    protected Transform _panelTransform;
    protected void OnEnable()
    {
        _showButton.onClick.AddListener(Show);
        _hideButton.onClick.AddListener(Hide);
    }
    protected void OnDisable()
    {
        _showButton.onClick.RemoveListener(Show);
        _hideButton.onClick.RemoveListener(Hide);
    }
    private void Show()
    {
        _menuPanel.Show();
        Shown?.Invoke();
    }
    private void Hide()
    {
        _menuPanel.Hide();
        Hid?.Invoke();
    }
}