using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace ColoringProject
{
    [CreateAssetMenu(fileName = "New TextureCollection", menuName = "TextureCollection")]
    public class SpriteCollection : ScriptableObject
    { 
        [SerializeField, Title("������ ���������")] public Sprite BackGroundOutlineSprite;
        [SerializeField, HorizontalGroup("BackgroundColor"), Title("���� ��������� ���������")] public List<Color> VisualBackgroundColors;
        [SerializeField, HorizontalGroup("BackgroundColor"), Title("���� ��������� �����")] public List<ColorsEnum> BackgroundColors;
        [SerializeField, HorizontalGroup("BackgroundColor"), Title("����������� ���������")] public List<Sprite> BackGroundSprite; 

        [SerializeField, Title("����� ������")] public Sprite Silhouette;
        [SerializeField, Title("������ ���������")] public Sprite OutlineSprite;
        [SerializeField, HorizontalGroup("Colors"), Title("��������� ���������� ����")] public List<Color> VisualColorsList;
        [SerializeField, HorizontalGroup("Colors"), Title("��������� ���� ��� �������.")] public List<ColorsEnum> ColorsList;
        [SerializeField, HorizontalGroup("Colors"), Title("��������� ������")] public List<Sprite> SpriteList;
    }
}