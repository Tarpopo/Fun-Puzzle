using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerClickArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [ShowInInspector]
    public bool Downed { get; set; }

    private void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Downed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Downed = false;
    }
}
