using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace ColoringProject
{
    [CreateAssetMenu(fileName = "New TextureCollection", menuName = "TextureCollection")]
    public class SpriteCollection : ScriptableObject
    { 
        [SerializeField, Title("Контур окружения")] public Sprite BackGroundOutlineSprite;
        [SerializeField, HorizontalGroup("BackgroundColor"), Title("Цвет окружения визуально")] public List<Color> VisualBackgroundColors;
        [SerializeField, HorizontalGroup("BackgroundColor"), Title("Цвет окружения озвуч")] public List<ColorsEnum> BackgroundColors;
        [SerializeField, HorizontalGroup("BackgroundColor"), Title("Изображение окружения")] public List<Sprite> BackGroundSprite; 

        [SerializeField, Title("Белый силуэт")] public Sprite Silhouette;
        [SerializeField, Title("Контур Персонажа")] public Sprite OutlineSprite;
        [SerializeField, HorizontalGroup("Colors"), Title("Назначить визуальный цвет")] public List<Color> VisualColorsList;
        [SerializeField, HorizontalGroup("Colors"), Title("Назначить цвет для озвучив.")] public List<ColorsEnum> ColorsList;
        [SerializeField, HorizontalGroup("Colors"), Title("Назначить спрайт")] public List<Sprite> SpriteList;
    }
}