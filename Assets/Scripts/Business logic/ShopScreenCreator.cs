using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreenCreator : MonoBehaviour
{
    public event System.Action OnSubscriptionAdOpened;
    public event System.Action OnSubscriptionAdClosed;

    [SerializeField]
    private GameObject SubAdBoxPrefab;
    [SerializeField]
    private GameObject captchaPrefab;

    private GameObject currentSubBox;
    private GameObject currentCaptchaCheck;
    
    [HideInInspector]
    public bool IsSubBoxOpened = false;
    [HideInInspector]
    public bool IsAnimation;
    [HideInInspector]
    public bool captchaIsChecked;
    [HideInInspector]
    public bool shopScreenHided;

    public void ShowSubAd()
    {
        if (currentSubBox == null && !IsSubBoxOpened && !IsAnimation)
        {
            createShopBox();
        }
    }

    private void createShopBox()
    {
        IsAnimation = true;
        currentSubBox = Instantiate(SubAdBoxPrefab, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        currentSubBox.transform.SetAsLastSibling();
        currentSubBox.GetComponent<ApearAnimation>().Show(() => { OnSubscriptionAdOpened?.Invoke(); IsSubBoxOpened = true; IsAnimation = false; });
        currentSubBox.GetComponent<BoxBase>().OnClose += () => { CloseSubAd(); };
        currentSubBox.GetComponent<ShopScreen>().OnBuyBtn += InitializeCaptchaCheck;
    }

    private void InitializeCaptchaCheck()
    {
        hideSubAd(createCaptchaCheck);
    }

    private void createCaptchaCheck()
    {
        currentCaptchaCheck = Instantiate(captchaPrefab, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        currentCaptchaCheck.GetComponent<CustomCaptcha>().Open(CaptchaChecked);
    }



    public void CloseSubAd()
    {
        if (IsSubBoxOpened && !IsAnimation)
        {
            IsAnimation = true;
            currentSubBox.GetComponent<ApearAnimation>().Hide(() => 
            {
                IsSubBoxOpened = false;
                Destroy(currentSubBox);
                currentSubBox = null;
                OnSubscriptionAdClosed?.Invoke();
                IsAnimation=false;
            });      
        }
    }


    private void hideSubAd(System.Action callback)
    {
        if (!shopScreenHided && !IsAnimation)
        {
            IsAnimation = true;
            currentSubBox.GetComponent<ApearAnimation>().Hide(() =>
            {
                callback?.Invoke();
                shopScreenHided = true;
                IsAnimation = false;
                currentSubBox.SetActive(false);
            });
        }
    }


    private void unhideSubAd()
    {
        if (shopScreenHided && !IsAnimation)
        {
            currentSubBox.SetActive(true);
            IsAnimation = true;
            currentSubBox.GetComponent<ApearAnimation>().Show(() =>
            {
                shopScreenHided = false;
                IsAnimation = false;
            });
        }
    }



    private void CaptchaChecked(bool IsRight)
    {
        
        if (IsRight)
        {
            CaptchaResolved();
        }
        if(currentCaptchaCheck!=null) Destroy(currentCaptchaCheck);
        unhideSubAd();
    }


    private void CaptchaResolved()
    {
        //todo: add buy system
    }


}
