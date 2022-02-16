using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsCreator : MonoBehaviour
{
    [SerializeField]
    GameObject RateUsBoxPref;
    
    GameObject currentBox;
    
    public void Start()
    {
        Invoke(nameof(CheckRateUs),20);
        // if(!RateUsScreenController.RateIsShowed && RateUsScreenController.RateTimeExpired)
        // {
        //     stateChanger.OnGameStateChanged += CheckRateUs;
        // }
    }
    
    // private void CheckRateUs(GameState state)
    // {
    //     if(state == GameState.AnimationOpening && !subscriptionStore.IsSubBoxOpened && !subscriptionStore.IsAnimation)
    //     {
    //         currentBox = Instantiate(RateUsBoxPref, GameObject.FindGameObjectWithTag("MainCanvas").transform);
    //         var box = currentBox.GetComponent<RateUsScreenController>();
    //         box.Open();
    //         box.OnClose += destroyRateUs;
    //     }
    // }
    private void CheckRateUs()
    { 
        currentBox = Instantiate(RateUsBoxPref, GameObject.FindGameObjectWithTag("MainCanvas").transform); 
        var box = currentBox.GetComponent<RateUsScreenController>();
        box.Open();
        box.OnClose += destroyRateUs;
    }
    
    private void destroyRateUs()
    {
        if (currentBox != null)
        {
            Destroy(currentBox);
            currentBox = null;
        }
    }

}
