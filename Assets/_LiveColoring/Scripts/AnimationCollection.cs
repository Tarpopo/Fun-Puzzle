using Sirenix.OdinInspector;
using UnityEngine;

namespace ColoringProject
{
    [CreateAssetMenu(fileName = "New AnimationCollection", menuName = "AnimationCollection")]
    public class AnimationCollection : ScriptableObject
    {
        [SerializeField, Title("Задний фон")] public Sprite Background;
        [SerializeField, Title("Префаб персонажа")] public CharAnimationPlay AnimationPrefab;
        [SerializeField, Title("Количество анимаций")] public int AnimCount = 3;

    }
}