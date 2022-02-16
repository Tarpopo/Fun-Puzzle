using UnityEngine;

namespace ColoringProject
{
    public class SceneSwitchButons : MonoBehaviour
    {
        public void ReturnToMenu()
        {
            SingletoneGameLogic.Instance.ReturnToMenu();
        }

        public void LoadNextCharacter()
        {
            SingletoneGameLogic.Instance.LoadNextCharacter();
        }
    }

}