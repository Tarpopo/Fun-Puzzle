using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PanelComponent : MonoBehaviour
{
    //[SerializeField] private GameObject _backGroundPanel;
    [SerializeField] private int _frames;
    
    [SerializeField] private float _scale;
    //private Coroutine _changeSize;
    private bool _isActive=true;

    public void SetActive()
    {
        if (_isActive)
        {
            transform.localScale=Vector2.zero;
            gameObject.SetActive(_isActive);
        }
        
        //_backGroundPanel.SetActive(_isActive); 
        var scale = _isActive?Vector2.one:Vector2.zero;
        _isActive = !_isActive;
        StartCoroutine(CorroutinesKit.ChangeScaleWithFrames(transform,scale*_scale, _frames,_isActive));
    }

    public void SetActiveWithoutScale()
    {
        gameObject.SetActive(_isActive);
        _isActive = !_isActive;
    }

}
