using UnityEngine;
using TMPro;
[System.Serializable]
public class StringView
{
    [SerializeField]
    private TextMeshProUGUI _stringView;
    public void SetString(string name)
    {
        _stringView.text = name;
    }
}