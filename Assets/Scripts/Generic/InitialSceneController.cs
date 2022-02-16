using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneController : MonoBehaviour
{
    public static bool IsInitialSceneLoaded = false;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !IsInitialSceneLoaded)
        {
            IsInitialSceneLoaded = true;
            SceneManager.LoadScene(1);
        }
        else if (!IsInitialSceneLoaded)
        {
            SceneManager.LoadScene(0);
        }
    }
}
