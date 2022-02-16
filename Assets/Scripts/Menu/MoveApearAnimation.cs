using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveApearAnimation : ApearAnimation
{
    [SerializeField]
    float XOffset;

    Vector3 origPos;


    public int frames = 20;

    private void Awake()
    {
        origPos = Vector3.zero;
    }

    public override void Show(System.Action callBack=null)
    {
        if (IsShowed == false)
        {
            StartCoroutine(showScreen(callBack));
            IsShowed = true;
        }
    }

    IEnumerator showScreen(System.Action callBack=null)
    {
        var pos = targetObject.transform.position;
        var offset = XOffset;
        targetObject.transform.localPosition = new Vector3(pos.x + XOffset, pos.y, pos.z);
        float dd = XOffset / frames;
        for (int i = 0; i < frames; i++)
        {
            offset -= dd;
            targetObject.transform.position = new Vector3(pos.x + offset, pos.y, pos.z);
            yield return new WaitForEndOfFrame();
        }
        targetObject.transform.position = pos;
        callBack?.Invoke();
    }

    IEnumerator hideScreen(System.Action callBack = null)
    {
        var pos = targetObject.transform.position;
        float offset = 0;
        targetObject.transform.localPosition = origPos;
        float dd = XOffset / frames;
        for (int i = 0; i < frames; i++)
        {
            offset += dd;
            targetObject.transform.position = new Vector3(pos.x + offset, pos.y, pos.z);
            yield return new WaitForEndOfFrame();
        }
        targetObject.transform.localPosition = Vector3.zero;
        IsShowed = false;
        callBack?.Invoke();
    }

    public override void Hide(System.Action callBack = null)
    {
        if (IsShowed)
        {
            StopAllCoroutines();
            StartCoroutine(hideScreen(callBack));
        }
    }
}
