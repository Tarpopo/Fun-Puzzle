using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTranslate : MonoBehaviour
{
    [SerializeField]
    private RectTransform _transform, _canvas;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite _endSprite;

    [SerializeField]
    private ParticleSystem _startDust;

    public event Action OnScreenClosed;

    public IEnumerator TranslateAnim()
    {
        _transform.gameObject.SetActive(true);

        int steps = 30;

        Vector2 delta = new Vector2(_canvas.sizeDelta.x * 3, 0) / steps;

        for (int i = 0; i < steps; i++)
        {
            _transform.sizeDelta += delta;

            yield return null;
        }

        _startDust.Stop();

        _image.sprite = _endSprite;

        OnScreenClosed?.Invoke();

        _transform.anchorMin = _transform.anchorMax = new Vector2(1f, 0.5f);
        _transform.anchoredPosition = Vector2.zero;
        _transform.pivot = new Vector2(0.4f, 0.5f);

        for (int i = 0; i < steps; i++)
        {
            _transform.sizeDelta -= delta;

            yield return null;
        }

        _transform.gameObject.SetActive(false);
    }
}
