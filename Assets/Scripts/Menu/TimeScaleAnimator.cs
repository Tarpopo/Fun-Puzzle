using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleAnimator : MonoBehaviour
{
    [SerializeField]
    private ScaleBar scaleBar;

    private bool IsAnimation;

    private Coroutine scaleAnimation;

    public void StartAnimation(float time, System.Action callback=null)
    {
        if(!IsAnimation) scaleAnimation = StartCoroutine(timeAnimate(time, callback));
    
    }


    IEnumerator timeAnimate(float time, System.Action callback)
    {
        IsAnimation = true;
        float startTime = Time.time;
        float endTime = startTime + time;

        while (Time.time <= endTime)
        {
            yield return null;
            float t = Time.time- startTime;
            if (t > time) t = time;
            scaleBar.updateScale(t / time, 1, true);
        }
        IsAnimation = false;
        callback?.Invoke();
    }

    public void StopAnimation()
    {
        StopCoroutine(scaleAnimation);
    }

    public bool IsAnimating()
    {
        return IsAnimation;
    }
}
