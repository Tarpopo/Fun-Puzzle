using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFromVal : MonoBehaviour
{
    private Image _image;

    [SerializeField]
    private string _val;

    [SerializeField]
    private Sprite[] _sprites;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _image.sprite = _sprites[PlayerPrefs.GetInt(_val)];
    }
}
