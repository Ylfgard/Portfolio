using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombinationBook
{
    public class CombinationPaintSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _paintsParent;
        [SerializeField] private GameObject _paintPref;

        public void Initialize(Transform parent, Combination combination)
        {
            _transform.SetParent(parent);
            foreach(Paint paint in combination.Paints)
            {
                PaintInitilizer initilizer = Instantiate(_paintPref, _paintsParent.position, Quaternion.identity).GetComponent<PaintInitilizer>();
                initilizer.Initialize(_paintsParent, paint.Color);
            }
        }
    }
}

