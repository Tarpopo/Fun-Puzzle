using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RateStar : MonoBehaviour, IPointerClickHandler
{
    public Image starImage;

    [SerializeField]
    RateUsScreenController screenController;

    public void OnPointerClick(PointerEventData eventData)
    {
        screenController.OnStarClicked(this);
    }
}
