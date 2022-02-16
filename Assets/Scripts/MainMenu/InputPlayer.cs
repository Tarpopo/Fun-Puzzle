using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.Events;

public class InputPlayer : MonoBehaviour, IBeforeEventChecker
{

    public  bool Downed { get; private set; }
    private bool _isSwipe;
    //private  int _touchCount;

    public Vector2 TouchPos { get; private set; }

    public UnityEvent OnSwipeLeft, OnSwipeRight;
    
    public Vector3 LastTouchWorldPosition { get; private set; }
    
    private void Update()
    {
        if (!IsTouched()) return;
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _isSwipe = false;
        }
        else
        {
            if (_isSwipe) return;
            TouchPos = Input.GetTouch(0).position;
            if (Input.GetTouch(0).deltaPosition.x < -30)
            {
                _isSwipe = true;
                OnSwipeLeft?.Invoke();
            }
            else if (Input.GetTouch(0).deltaPosition.x > 30)
            {
                _isSwipe = true;
                OnSwipeRight?.Invoke();
            }
        }


    }
    
    private bool IsTouched()
    {
    
        return Input.touchCount > 0;
        
    }

    public Vector2 GetTouchWorldPos()
    {
        if (!IsTouched())
        {
            return LastTouchWorldPosition;
        }
        
        if(Input.touchCount > 0)
        {
            LastTouchWorldPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        
        return LastTouchWorldPosition;
    }

    public bool IsEventHappens()
    {
        return _isSwipe;
    }
}
