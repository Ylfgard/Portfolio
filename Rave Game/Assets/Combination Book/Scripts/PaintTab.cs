using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombinationBook
{
    public class PaintTab : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _colorOutput;

        public void Initialize(Transform parent, ToggleGroup toggleGroup, Page page, Paint paint)
        {
            _transform.SetParent(parent);
            _toggle.group = toggleGroup;
            _colorOutput.color = paint.Color;
            _toggle.onValueChanged.AddListener(page.gameObject.SetActive);
        }

        public void MakeInterractable()
        {
            _toggle.interactable = true;
        }
    }
}
