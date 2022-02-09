using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombinationBook
{
    public class PaintInitilizer : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Image _colorOutput;

        public void Initialize(Transform parent, Color color)
        {
            _transform.SetParent(parent);
            _colorOutput.color = color;
        }
    }
}

