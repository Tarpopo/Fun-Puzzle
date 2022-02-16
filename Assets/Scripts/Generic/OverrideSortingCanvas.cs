using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideSortingCanvas : MonoBehaviour
{
    [SerializeField]
    public Canvas canvas;

    private void OnEnable()
    {
        canvas.overrideSorting = true;
    }
}
