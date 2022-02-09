using UnityEngine;
using TMPro;
[System.Serializable]
public class NumberView
{
    [SerializeField]
    private TextMeshProUGUI _numberView;
    public void SetNumber(int number)
    {
        _numberView.text = number.ToString();
    }
}
