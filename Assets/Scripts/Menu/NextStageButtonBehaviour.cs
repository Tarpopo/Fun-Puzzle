using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageButtonBehaviour : MonoBehaviour
{
    //public CharacterCreator creator;
    public GameObject button;

    private bool showingStoreButton;

    //[SerializeField]
   //ShopScreenCreator storeCreator;

    //[SerializeField]
    //NewAnimationEffect newAnimationEffect;

    [SerializeField]
    GameObject openStoreBtn;


    private void Awake()
    {
        //newAnimationEffect.OnAdCharacterSpawned += showStoreBtn;
    }

    public void Init()
    {
        if (PlayerPrefs.GetInt("firstAnimationUse") != 1 || showingStoreButton)
        {
            button.SetActive(false);
            //creator.GetCharacter().GetComponent<Character>().OnUseAnimation += ShowButton;
        }
    }


    public void ShowButton()
    {
        if (openStoreBtn.activeSelf)
        {
            showingStoreButton = false;
            openStoreBtn.SetActive(false);
        }

        PlayerPrefs.SetInt("firstAnimationUse", 1);
        button.SetActive(true);
     
    }


    private void showStoreBtn()
    {
        showingStoreButton = true;
        button.SetActive(false);
        openStoreBtn.SetActive(true);
        openStoreBtn?.GetComponent<ApearAnimation>().Show();
        //storeCreator.OnSubscriptionAdOpened += hideStoreBtn;
        StartCoroutine(hideWithDelay());
    }

    private void hideStoreBtn()
    {
        if (openStoreBtn.activeSelf)
        {
            openStoreBtn?.GetComponent<ApearAnimation>().Hide(() =>
            {
                showingStoreButton = false;
                button.SetActive(true);
                openStoreBtn.SetActive(false);
            });
        }

    }

    private IEnumerator hideWithDelay()
    {
        yield return new WaitForSeconds(5.5f);
        if (openStoreBtn.activeSelf) hideStoreBtn();
    }
}
