using ColoringProject;
using UnityEngine;

public class SelectTopic : MonoBehaviour
{
    public void SelectAndSetTopic(int topic)
    {
        SingletoneGameLogic.Instance.SetupImagesIntoCarousel(topic);
    }
}
