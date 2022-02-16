using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageClick : MonoBehaviour, IPointerDownHandler
{
    public Image img;

    void Start()
    {
        img.alphaHitTestMinimumThreshold = 0.5f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       // GameObject.Find("GameObject").GetComponent<game>().estado_name(this.gameObject.name);
    }

}