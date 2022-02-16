using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateUsScreenController : MonoBehaviour
{

    public ApearAnimation apearAnimation;
    public GameObject screenObject;

    [SerializeField]
    private List<RateStar> stars;

    [SerializeField] Sprite activeStar;
    [SerializeField] Sprite inactiveStar;
    [SerializeField] Button rateButton;
    [SerializeField] GameObject buttonEffect;
    [SerializeField] Text rateText;
    [SerializeField] Color closeColor;
    static float secondsDelay = 8;
    int rateValue = 0;

    private bool disabled;

    public System.Action OnOpen;
    public System.Action OnClose;

    private static int lastRateTime
    {
        get
        {
            return PlayerPrefs.GetInt("ratetime");
        }

        set
        {
            PlayerPrefs.SetInt("ratetime", value);
        }
    }

    public static bool RateTimeExpired
    {
        get { return System.DateTime.Now.DayOfYear != lastRateTime; }
    }

    public static bool RateIsShowed
    {
        get
        {
            return PlayerPrefs.GetInt("rated") == 1;
        }
        
    }
    


    public void Open()
    {
        ClearRate();
        if (!apearAnimation.IsShowed)
        {
            screenObject.SetActive(true);
            apearAnimation.Show(()=> { OnOpen?.Invoke(); });
        }
        rateButton.interactable = false;
        rateText.color = closeColor;
    }


    public void Close()
    {
        if (apearAnimation.IsShowed)
        {
            lastRateTime = System.DateTime.Now.DayOfYear;
            apearAnimation.Hide(() => { screenObject.SetActive(false); OnClose?.Invoke(); });
        }
    }

    public void OnStarClicked(RateStar star)
    {
        buttonEffect.SetActive(true);
        rateText.color = Color.white;
        SetRate(star);
    }

    private void ClearRate()
    {
        rateValue = 0;
        foreach (var st in stars)
        {
            st.starImage.sprite = inactiveStar;
        }
    }

    private void SetRate(RateStar star)
    {
        ClearRate();
        rateValue = 0;

        for(int i=1; i<=stars.Count; i++)
        {
            stars[i-1].starImage.sprite = activeStar;
            stars[i-1].starImage.color = Color.white;
            rateValue = i;
            if (star == stars[i-1]) break;
        }
        rateButton.interactable = true;
    }

    public void Rate()
    {
        lastRateTime = System.DateTime.Now.DayOfYear;

        print(rateValue);
        if (rateValue == 0)
        {
            Close();
            return;
        }

        PlayerPrefs.SetInt("rated", 1);


        if (rateValue == 5)
        {
            redirectToRating();           
        }
        Close();
    }

    private void redirectToRating()
    {
        //todo: add rate redirect
    }



}
