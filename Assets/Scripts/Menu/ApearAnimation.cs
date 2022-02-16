using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ApearAnimation : MonoBehaviour
{
    [HideInInspector]
    public bool IsShowed;

    [SerializeField]
    protected GameObject targetObject;


    public abstract void Show(System.Action callBack = null);
    public abstract void Hide(System.Action callBack = null);


}
