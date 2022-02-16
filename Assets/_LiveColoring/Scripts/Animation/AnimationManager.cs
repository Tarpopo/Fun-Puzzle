using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ColoringProject
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance;
        [SerializeField] private AnimationCollection animationCollection;
        public PlayerClickArea playerClickArea;

        private CharAnimationPlay _characer;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform buttonsParent; 

        private void Awake()
        {
            if(Instance!=null) Debug.LogError("2 Instances");
            Instance = this;
            if(SingletoneGameLogic.Instance!=null) animationCollection = SingletoneGameLogic.Instance.CurrentAnimationCollection;
            gameObject.SetActive(false);
        }

        private void Start()
        {
            playerClickArea = FindObjectOfType<PlayerClickArea>();
            _characer = Instantiate(animationCollection.AnimationPrefab, spawnPoint);
            _characer.manager = this; 
            GetComponent<Image>().sprite = animationCollection.Background;
            MakeButtonsActive();
        }

        private void MakeButtonsActive()
        {
            int activateCount = animationCollection.AnimCount;

            foreach (Transform button in buttonsParent) //enable existing animations
            {
                button.gameObject.SetActive(activateCount > 0);
                activateCount--;
            }

            List<SaveStatus> list = SingletoneGameLogic.Instance.GetCurrentAnimationStatuses(); //block unavailable animation buttons
            for (int index = 0; index < list.Count; index++)
            {
                var currentAnimationStatus = list[index];
                if (currentAnimationStatus != SaveStatus.Opened) buttonsParent.GetChild(index).GetComponent<Button>().interactable = false;
            }

        }

        public void PlayEnumAnim(int animationName)
        {
            _characer.AnimPlay("Anim_" + animationName);
        }

        private void Update()
        {
        }
    }
}
