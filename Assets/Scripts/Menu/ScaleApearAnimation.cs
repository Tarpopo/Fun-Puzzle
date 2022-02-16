using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleApearAnimation : ApearAnimation
{
    public int frames = 20;

    public override void Show(Action callBack = null)
    {
        if (!IsShowed)
        {
            targetObject.SetActive(true);
            StartCoroutine(showScreen(callBack));
        }
    }

    public override void Hide(Action callBack = null)
    {
        if (IsShowed)
        {
            targetObject.SetActive(true);
            StartCoroutine(hideScreen(callBack));
        }
    }



    private IEnumerator showScreen(Action callback)
    {
        targetObject.transform.localScale = Vector3.zero;
        
        yield return StartCoroutine(CoroutinesUtil.ScalerAnim(targetObject.transform, Vector3.one, frames));
        IsShowed = true;
        callback?.Invoke();
        print("showed");
    }

    private IEnumerator hideScreen(Action callback)
    {
        targetObject.transform.localScale = Vector3.one;

        yield return StartCoroutine(CoroutinesUtil.ScalerAnim(targetObject.transform, Vector3.zero, frames));
        IsShowed = false;
        print("hided");
        callback?.Invoke();
        targetObject.SetActive(false);
    }
}
