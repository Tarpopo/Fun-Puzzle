using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryBtns : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] _btns;

    [SerializeField]
    private float _scaler;

    [SerializeField]
    private int _frames;

    private Vector3 _origSize, _targetSize;

    private int _currentId;

    public event Action OnBtnChanged;

    private void Awake()
    {
        _origSize = _btns[0].sizeDelta;

        _targetSize = _origSize * _scaler;

        StartCoroutine(CoroutinesUtil.ScalerSizeDelta(_btns[0], _targetSize, _frames, BtnChanged));
    }

    public void ClickOnButton(int id)
    {
        if (_currentId == id)
            return;

        StopAllCoroutines();

        StartCoroutine(SwitchBtns(_currentId, id));

        _currentId = id;
    }

    private IEnumerator SwitchBtns(int oldId, int newId)
    {
        yield return StartCoroutine(CoroutinesUtil.ScalerSizeDelta(_btns[oldId], _origSize, _frames, null));

        StartCoroutine(CoroutinesUtil.ScalerSizeDelta(_btns[newId], _targetSize, _frames, BtnChanged));
    }

    private void BtnChanged()
    {
        OnBtnChanged?.Invoke();
    }
}
