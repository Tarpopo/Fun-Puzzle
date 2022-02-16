using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstLoadChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFirstLoad;
    public bool IsFirstLoad { get; private set; }

    private void Start()
    {
        IsFirstLoad = GetSave();
        if (IsFirstLoad == false) return;
        _onFirstLoad?.Invoke();
    }

    private bool GetSave()
    {
        return true;
    }
    
}
