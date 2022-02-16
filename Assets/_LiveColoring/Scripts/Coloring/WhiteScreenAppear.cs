using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ColoringProject
{
    public class WhiteScreenAppear : MonoBehaviour
    {
        public static WhiteScreenAppear Instance;
        [Tooltip("Появляется после завершения уровня")]
        [SerializeField] [Required] private Image winWhiteImage; // 

        private void OnEnable()
        {
            Instance = this; 
            winWhiteImage = transform.GetChild(0).GetComponent<Image>();
        }


         
        public void AppearScreen(bool isImageApearing)
        { 
            StartCoroutine(WinImageAppear(isImageApearing));
        }

        private IEnumerator WinImageAppear(bool isImageApearing)
        {
            Color color = winWhiteImage.color;
            float timer = 0;
            while (timer <= 1)
            {
                timer += Time.deltaTime / 2;
                if (isImageApearing)
                    winWhiteImage.color = new Color(color.r, color.g, color.b, timer);
                else
                    winWhiteImage.color = new Color(color.r, color.g, color.b, 1 - timer);
                yield return new WaitForEndOfFrame();
            }
            if (isImageApearing)
                winWhiteImage.color = new Color(color.r, color.g, color.b, 1);
            else
                winWhiteImage.color = new Color(color.r, color.g, color.b, 0);
        }
    }
}