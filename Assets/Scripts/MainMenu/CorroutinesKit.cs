using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorroutinesKit : MonoBehaviour
{
    // public static IEnumerator ChangePosition(Transform startTransform, Vector2 endPosition, float speed)
    // {
    //     while (Vector2.Distance(startTransform.position,endPosition)>0)
    //     {
    //         startTransform.position = Vector2.MoveTowards(startTransform.position, endPosition,speed);
    //         yield return null;
    //     }
    //     
    // }
    public static IEnumerator ChangePosition(Transform from, Vector3 target, int frames)
    {
        Vector3 delta = (target - from.position) / frames;

        for (int i = 0; i < frames; i++)
        {
            from.position += delta;
            yield return null;
        }
    }
    public static IEnumerator ChangePosition(RectTransform from, Vector2 target, int frames)
    {
        Vector2 delta = (target - from.anchoredPosition) / frames;

        for (int i = 0; i < frames; i++)
        {
            from.anchoredPosition += delta;
            yield return null;
        }
    }
    
    // public static IEnumerator MoveGlobal(Transform tr, Vector3 target, int frames)
    // {
    //     Vector3 delta = (target - tr.position) / frames;
    //
    //     for (int i = 0; i < frames; i++)
    //     {
    //         tr.position += delta;
    //         yield return null;
    //     }
    //     
    // }
    public static IEnumerator ChangeScale(Transform from, Vector3 targetScale, int frames,bool deactivateObject=false)
    {
        Vector3 delta = (targetScale - from.localScale) / frames;

        for (int i = 0; i < frames; i++)
        {
            from.localScale += delta;
            yield return null;
        }
        if(deactivateObject) from.gameObject.SetActive(false);
    }
    
    // public static IEnumerator ChangeScale(Transform from, Vector2 to,float speed, bool deactivateObject=false)
    // {
    //     while (Vector2.Distance(from.localScale,to)>0)
    //     {
    //         from.localScale = Vector2.MoveTowards(from.localScale,to,speed);
    //         yield return null;
    //     }
    //     if(deactivateObject) from.gameObject.SetActive(false);
    //     
    // }
    public static IEnumerator ChangeScaleWithFrames(Transform from, Vector3 to,int frames, bool deactivateObject=false)
    {
        var delta= (to-from.localScale)/frames;
        for (var i=0; i<frames; i++)
        {
            from.localScale += delta;
            yield return null;
        }
    
        if(deactivateObject) from.gameObject.SetActive(false);
    }
    public static IEnumerator PlayDoubleScale(Transform from, Vector3 toOne,Vector3 toTwo,Vector2 endPosition,int frames)
    {
        Vector3 delta = (toOne - from.localScale) / frames;

        for (int i = 0; i < frames; i++)
        {
            from.localScale += delta;
            yield return null;
        }
        
        delta = (toTwo - from.localScale) / frames;
        from.position = endPosition;
        
        for (int i = 0; i < frames; i++)
        {
            from.localScale += delta;
            yield return null;
        }
        
    }
}
