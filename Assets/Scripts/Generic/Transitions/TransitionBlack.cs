using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionBlack : MonoBehaviour, ITransition
{
    [SerializeField]
    Image background;

    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main.GetComponent<Camera>();

        transform.position = new Vector3(0, 0, -1000);
    }

    public void StartAnim(bool close, Action onEndTransition)
    {
        StartCoroutine(TransitionAnim(close, onEndTransition));
    }

    IEnumerator TransitionAnim(bool toBlack, Action onEndTransition)
    {
        Color c = background.color;

        if(toBlack)
        {
            c.a = 0;
        }
        else
        {
            c.a = 1;
        }

        int steps = 15;

        float dA = 1f / steps;

        if(!toBlack)
        {
            dA *= -1;
        }

        background.color = c;

        for (int i = 0; i < steps; i++)
        {
            c.a += dA;
            background.color = c;

            yield return null;
        }

        onEndTransition?.Invoke();

        if(!toBlack)
        {
            Destroy(gameObject);
        }
    }
}
