using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBase : MonoBehaviour
{
    public System.Action OnOpen;
    public System.Action OnClose;

    public void Open()
    {
        OnOpen?.Invoke();
    }


    public void Close()
    {
        OnClose?.Invoke();
    }


}
