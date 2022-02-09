using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonMenu : MonoBehaviour
{
    [SerializeField]
    private Panel _buttonMenuPanel;
    [SerializeField]
    private List<Button> _hideButtons;
    [SerializeField]
    private List<Button> _showButtons;
    private void OnEnable()
    {
        foreach (Button hideButton in _hideButtons)
            hideButton.onClick.AddListener(_buttonMenuPanel.Hide);
        foreach (Button showButton in _showButtons)
            showButton.onClick.AddListener(_buttonMenuPanel.Show);
    }
    private void OnDisable()
    {
        foreach (Button hideButton in _hideButtons)
            hideButton.onClick.RemoveListener(_buttonMenuPanel.Hide);
        foreach (Button showButton in _showButtons)
            showButton.onClick.RemoveListener(_buttonMenuPanel.Show);
    }
}