using Sirenix.OdinInspector;
using UnityEngine;

namespace ColoringProject
{
    [CreateAssetMenu(fileName = "New AnimationCollection", menuName = "AnimationCollection")]
    public class AnimationCollection : ScriptableObject
    {
        [SerializeField, Title("������ ���")] public Sprite Background;
        [SerializeField, Title("������ ���������")] public CharAnimationPlay AnimationPrefab;
        [SerializeField, Title("���������� ��������")] public int AnimCount = 3;

    }
}