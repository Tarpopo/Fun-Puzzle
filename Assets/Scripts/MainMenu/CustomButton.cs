using Assets.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private IBeforeEventChecker _beforeChecker;
    [SerializeField] private GameObject _checkerObject;
    [SerializeField] private UnityEvent _down, _up;


    private void Start()
    {
        if (_checkerObject == null) return;
        _beforeChecker=_checkerObject.GetComponent<IBeforeEventChecker>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_beforeChecker!=null&&_beforeChecker.IsEventHappens()) return;
        _down?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_beforeChecker!=null&&_beforeChecker.IsEventHappens()) return;
        _up?.Invoke();
    }
}
