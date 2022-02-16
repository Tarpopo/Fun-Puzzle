using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCircles : MonoBehaviour
{
    [SerializeField]
    private Image[] _circles;

    private Vector3 _origScale;

    [SerializeField]
    private Vector3 _targetScale;

    private IEnumerator Start()
    {
        _origScale = _circles[0].transform.localScale;

        Color targetColor = Color.white;
        targetColor.a = 0;

        int frames = 33;

        for (int i = 0; i < _circles.Length; i++)
        {
            _circles[i].transform.localScale = _origScale;
            _circles[i].color = Color.white;

            StartCoroutine(CoroutinesUtil.ScalerAnim(_circles[i].transform, _targetScale, frames));
            StartCoroutine(CoroutinesUtil.ImageToColorAnim(_circles[i], targetColor, frames));

            for (int k = 0; k < frames / 3; k++)
            {
                yield return null;
            }

           // frames = 20;

            _targetScale *= 0.9f;
        }
    }
}
