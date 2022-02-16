using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDeadEffect : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite[] _animationSprites;

    [SerializeField]
    private float _delay;

    private bool _working;

    public void StartEffect()
    {
        if (_working)
            return;

        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        _working = true;

        _image.enabled = true;

        for (int i = 0; i < _animationSprites.Length; i++)
        {
            _image.sprite = _animationSprites[i];
            yield return new WaitForSeconds(_delay);
        }

        _image.enabled = false;

        _working = false;
    }
}
