using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutinesUtil : MonoBehaviour
{
    public static IEnumerator ScalerAnim(Transform tr, Vector3 targetScale, int frames)
    {
        Vector3 delta = (targetScale - tr.localScale) / frames;

        for (int i = 0; i < frames; i++)
        {
            tr.localScale += delta;
            yield return null;
        }
    }

    public static IEnumerator ScalerAnim(Transform tr, Vector3 targetScale, int frames, Action EndAction)
    {
        Vector3 delta = (targetScale - tr.localScale) / frames;

        for (int i = 0; i < frames; i++)
        {
            tr.localScale += delta;
            yield return null;
        }

        EndAction?.Invoke();
    }

    public static IEnumerator ImageToColorAnim(Image image, Color target, int frames)
    {
        Color deltaColor = (target - image.color) / frames;

        Color tempColor = image.color;

        for (int i = 0; i < frames; i++)
        {
            tempColor += deltaColor;
            image.color = tempColor;

            yield return null;
        }
    }

    public static IEnumerator ImageToColorAnim(RawImage image, Color target, int frames)
    {
        Color deltaColor = (target - image.color) / frames;

        Color tempColor = image.color;

        for (int i = 0; i < frames; i++)
        {
            tempColor += deltaColor;
            image.color = tempColor;

            yield return null;
        }
    }

    public static IEnumerator MoveLocal(Transform tr, Vector3 target, int frames, Action EndAction)
    {
        Vector3 delta = (target - tr.localPosition) / frames;

        for (int i = 0; i < frames; i++)
        {
            tr.localPosition += delta;
            yield return null;
        }

        EndAction?.Invoke();
    }

    public static IEnumerator ScalerSizeDelta(RectTransform tr, Vector2 target, int frames, Action EndAction)
    {
        Vector2 delta = (target - tr.sizeDelta) / frames;

        for (int i = 0; i < frames; i++)
        {
            tr.sizeDelta += delta;
            yield return null;
        }

        EndAction?.Invoke();
    }

    public static IEnumerator MoveGlobal(Transform tr, Vector3 target, int frames, Action EndAction)
    {
        Vector3 delta = (target - tr.position) / frames;

        for (int i = 0; i < frames; i++)
        {
            tr.position += delta;
            yield return null;
        }

        EndAction?.Invoke();
    }

    public static IEnumerator RotateGlobalOnAngle(Transform tr, float angleZ, int frames, Action EndAction)
    {
        angleZ /= frames;

        for (int i = 0; i < frames; i++)
        {
            tr.Rotate(0, 0, angleZ);
            yield return null;
        }

        EndAction?.Invoke();
    }

    public static IEnumerator Shake(Transform tr, int duration, int steps, float stepLen, Action end)
    {
        Vector3 origPos = tr.localPosition;

        Vector3 delta = new Vector3(stepLen, 0, 0);

        for (int k = 0; k < steps/2; k++)
        {
            tr.localPosition -= delta;

            yield return null;
        }

        for (int i = 0; i < duration; i++)
        {
            for (int k = 0; k < steps; k++)
            {
                tr.localPosition += delta;

                yield return null;
            }

            for (int k = 0; k < steps; k++)
            {
                tr.localPosition -= delta;

                yield return null;
            }
        }

        tr.localPosition = origPos;

        end?.Invoke();
    }
}
