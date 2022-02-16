using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class FingerShow : MonoBehaviour
{
    private Vector2 _startPosition;

    private RectTransform _rectTransform;
     
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.position;
    }

    [Button]
    public void ClickTo(Vector2 pos)
    {
        if (_clickCorotune != null) StopCoroutine(_clickCorotune);
        _clickCorotune = StartCoroutine(ClickAnimation(pos));
    }

    [Button]
    public void ClickTo(RectTransform rect)
    {
        if (_clickCorotune != null) StopCoroutine(_clickCorotune);
        _clickCorotune = StartCoroutine(ClickAnimation(rect.position));
    }

    private Coroutine _clickCorotune;

    private IEnumerator ClickAnimation(Vector2 clickPosition)
    {
        float time = 0;
        float pushUpOffset = 15;
        Vector2 fingerUpPosition = clickPosition + Vector2.up * pushUpOffset + Vector2.right;
        while (time < 1)
        { 
            _rectTransform.position = Vector2.Lerp(_startPosition, fingerUpPosition, time);

            ResetZPosition();
            time += Time.deltaTime / 1.5f; 
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < 1)
        {
            const float secondParts = 2;

            float lerpTime = time * secondParts % 1;

            if (time * secondParts < 1) _rectTransform.position = Vector2.Lerp(fingerUpPosition, clickPosition, lerpTime);
            else if (time * secondParts < 2) _rectTransform.position = Vector2.Lerp(clickPosition, fingerUpPosition, lerpTime);
            ResetZPosition();
            time += Time.deltaTime / 1f;

            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < 1)
        {
            _rectTransform.position = Vector2.Lerp(fingerUpPosition, _startPosition, time);
            ResetZPosition();
            time += Time.deltaTime / 1.5f;

            yield return new WaitForEndOfFrame();
        }

        _clickCorotune = null;
    }

    private void ResetZPosition()
    {
        _rectTransform.localPosition = new Vector3(_rectTransform.localPosition.x, _rectTransform.localPosition.y, 0);
        _rectTransform.localPosition = _rectTransform.localPosition;
    }
}