using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField]
    CustomCaptcha captcha;

    public event System.Action OnBuyBtn;
    public void Buy()
    {
        OnBuyBtn?.Invoke();
    }

}
