using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputDetector : MonoBehaviour
{
    [SerializeField] private SingleInputHandler _singleTouchHandler;
    
    void Update()
    {
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
            _singleTouchHandler.InputDown(inputPos);
        if(Input.GetMouseButtonUp(0))
            _singleTouchHandler.InputUp(inputPos);
        if(Input.GetMouseButton(0))
        {
            if(Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
                _singleTouchHandler.InputPress(inputPos);
            else
                _singleTouchHandler.InputMove(inputPos);
        }
    }
}
