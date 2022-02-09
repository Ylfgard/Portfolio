using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    [SerializeField] private SingleInputHandler _singleTouchHandler;

    void Update()
    {
        switch(Input.touchCount)
        {
            case 0: 
            return;
            
            case 1: 
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if(touch.phase == TouchPhase.Began)
                _singleTouchHandler.InputDown(touchPos);
            if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                _singleTouchHandler.InputUp(touchPos);
            if(touch.phase == TouchPhase.Stationary)
                _singleTouchHandler.InputPress(touchPos);
            if(touch.phase == TouchPhase.Moved)
                _singleTouchHandler.InputPress(touchPos);
            return;

            default:
            return;
        }
    }
}
