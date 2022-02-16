using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapParticlesEffect : MonoBehaviour, IPointerClickHandler
{

    private bool IsActive = false;

    [SerializeField]
    GameObject particlesEffect;

    [SerializeField]
    float destroyTime = 2f;

    public void OnPointerClick(PointerEventData eventData)
    {
            var obj = Instantiate(particlesEffect, transform);
            var pos = Camera.main.ScreenToWorldPoint(eventData.position);
            obj.transform.position = new Vector3(pos.x, pos.y, 0);
            Destroy(obj, destroyTime);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
