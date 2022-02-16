using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public event Action<Transform> OnTriggerEnter, OnTriggerExit;

    private List<Transform> _inTrigger = new List<Transform>(3);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter.Invoke(collision.transform);

        _inTrigger.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit.Invoke(collision.transform);

        _inTrigger.Remove(collision.transform);
    }

    public bool InTrigger(Transform tr)
    {
        return _inTrigger.Contains(tr);
    }
}
