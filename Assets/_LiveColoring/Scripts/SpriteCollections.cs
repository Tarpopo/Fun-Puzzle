using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColoringProject
{
    [CreateAssetMenu(fileName = "New TextureCollectionManager", menuName = "TextureCollectionManager")] 
    public class SpriteCollections : ScriptableObject
    {
        [SerializeField, Title("��������� ���")] public List<SpriteCollectionTopic> SpriteCollectionTopics;
    }

    [System.Serializable]
    public class SpriteCollectionTopic
    {
        [SerializeField, Title("��������� ���������"), HorizontalGroup("Character")] public List<SpriteCollection> SpriteCollectionsList;
        [SerializeField, Title("��������� ���������"), HorizontalGroup("Character")] public List<AnimationCollection> AnimationCollectionsList;
    }
}
