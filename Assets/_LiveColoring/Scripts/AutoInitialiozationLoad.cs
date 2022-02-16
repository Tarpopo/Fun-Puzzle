using ColoringProject;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoInitialiozationLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SingletoneGameLogic.Instance == null)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
