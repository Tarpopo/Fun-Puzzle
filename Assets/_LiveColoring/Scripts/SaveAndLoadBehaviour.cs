using System;
using System.Collections.Generic;
using ColoringProject;
using Sirenix.OdinInspector;
using UnityEngine;

public enum SaveStatus
{
    Opened,
    Closed,
    Premium,
}
 
[Serializable]
public class LevelSaving
{
    public List<AnimationsSaving> LevelsStatus = new List<AnimationsSaving>();
}
 
[Serializable]
public class AnimationsSaving
{
    public List<SaveStatus> AnimsStatus = new List<SaveStatus>();
}

public class SaveAndLoadBehaviour : MonoBehaviour
{
    public static SaveAndLoadBehaviour Instance;

    [SerializeField] public List<LevelSaving> TopicsStatus = new List<LevelSaving>();
    [SerializeField] private bool enableSavingAndLoading = true;
    [Button]
    public void SetupDefault(bool openEverything = false) 
    {
        SpriteCollections collections = SingletoneGameLogic.Instance.CurrentCollection;
        TopicsStatus.Clear();
        for (int topicInd = 0; topicInd < collections.SpriteCollectionTopics.Count; topicInd++) //настройка тем
        {
            LevelSaving levels = new LevelSaving();

            for (int levelInd = 0; levelInd < collections.SpriteCollectionTopics[topicInd].AnimationCollectionsList.Count; levelInd++)
            {  
                AnimationsSaving animations = new AnimationsSaving();
                 
                for (int animInd = 0; animInd < collections.SpriteCollectionTopics[topicInd].AnimationCollectionsList[levelInd].AnimCount; animInd++)
                {
                    if (openEverything)
                    {
                        animations.AnimsStatus.Add(SaveStatus.Opened);
                        continue;
                    }

                    if (levelInd > 2) //premium
                    {
                        animations.AnimsStatus.Add(SaveStatus.Premium);
                        continue;
                    }

                    if (levelInd>0) //closed
                    {
                        animations.AnimsStatus.Add(SaveStatus.Closed);
                        continue;
                    }
                    
                    animations.AnimsStatus.Add(SaveStatus.Opened);
                }
                levels.LevelsStatus.Add(animations);
            }

            TopicsStatus.Add(levels);
        }
    }

    [Button]
    public void Save()
    {
        ES3.Save("Progress", TopicsStatus); 
    }

    [Button]
    public void Load()
    {
        TopicsStatus = ES3.Load("Progress", TopicsStatus); 
    }

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        if(enableSavingAndLoading) Load();
    }

    private void OnApplicationQuit()
    {
        if (Instance != this) return;
        if (enableSavingAndLoading) Save();
    }
}
