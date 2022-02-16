using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleBar : MonoBehaviour
{
    public Image fullScale;
    public Image currentScale;


    public void updateScale(float scaleX=1, float scaleY=1, bool scaleWidthOnly=false)
    {
        RectTransform full = fullScale.rectTransform;
        RectTransform current = currentScale.rectTransform;

        float x = full.rect.width * scaleX;
        float y = full.rect.height * scaleY;

        if (scaleWidthOnly)
        {
            y = current.rect.height;
        }




        current.sizeDelta = new Vector2(x, y);
    }
}
