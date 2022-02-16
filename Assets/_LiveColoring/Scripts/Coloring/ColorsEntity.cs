using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColoringProject
{
    public enum ColorsEnum
    {
        None = 0,
        Red = 1,
        Orange = 2,
        Yellow = 3,
        Green = 4,
        Blue = 5,
        Cyan = 6,
        Purple = 7,
        White = 8,
        Black = 9,
        Brown = 10,
        Magenta = 11,
        Tan = 12,
        Olive = 13,
        Maroon = 14,
        Navy = 15,
        Aquamarine = 16,
        Turquoise = 17,
        Silver = 18,
        Lime = 19,
        Teal = 20,
        Indigo = 21,
        Violet = 22,
        Pink = 23,
        Gray = 24,
        LightBlue = 25,
        DarkBlue = 26,
        LightOrange = 27,
        DarkOrange = 28,
        LightRed,
        DarkRed,
        LightBrown,
        DarkBrown,
        LightGreen,
        DarkGreen,
        Beige,
        Lilac,
        LightLilac,
        DarkLilac,
        Ãinous,
    }

    public class ColorsEntity : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> voiceClips;

        public AudioClip GetAudioClip(ColorsEnum colorsEnum)
        {
            return null; //todo „ÓÎÓÒ‡ ˆ‚ÂÚÓ‚
            switch (colorsEnum)
            {
                case ColorsEnum.None:
                    break;
                case ColorsEnum.Red:
                    break;
                case ColorsEnum.Orange:
                    break;
                case ColorsEnum.Yellow:
                    break;
                case ColorsEnum.Green:
                    break;
                case ColorsEnum.Blue:
                    break;
                case ColorsEnum.Cyan:
                    break;
                case ColorsEnum.Purple:
                    break;
                case ColorsEnum.White:
                    break;
                case ColorsEnum.Black:
                    break;
                case ColorsEnum.Brown:
                    break;
                case ColorsEnum.Magenta:
                    break;
                case ColorsEnum.Tan:
                    break;
                case ColorsEnum.Olive:
                    break;
                case ColorsEnum.Maroon:
                    break;
                case ColorsEnum.Navy:
                    break;
                case ColorsEnum.Aquamarine:
                    break;
                case ColorsEnum.Turquoise:
                    break;
                case ColorsEnum.Silver:
                    break;
                case ColorsEnum.Lime:
                    break;
                case ColorsEnum.Teal:
                    break;
                case ColorsEnum.Indigo:
                    break;
                case ColorsEnum.Violet:
                    break;
                case ColorsEnum.Pink:
                    break;
                case ColorsEnum.Gray:
                    break;
                case ColorsEnum.LightBlue:
                    break;
                case ColorsEnum.DarkBlue:
                    break;
                case ColorsEnum.LightOrange:
                    break;
                case ColorsEnum.DarkOrange:
                    break;
                case ColorsEnum.LightRed:
                    break;
                case ColorsEnum.DarkRed:
                    break;
                case ColorsEnum.LightBrown:
                    break;
                case ColorsEnum.DarkBrown:
                    break;
                case ColorsEnum.LightGreen:
                    break;
                case ColorsEnum.DarkGreen:
                    break;
                case ColorsEnum.Beige:
                    break;
                case ColorsEnum.Lilac:
                    break;
                case ColorsEnum.LightLilac:
                    break;
                case ColorsEnum.DarkLilac:
                    break;
                case ColorsEnum.Ãinous:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(colorsEnum), colorsEnum, null);
            }
        }

        private const float Max = 255f; //MaxColorRGB

        //public static Color GetColorFromEnum(ColorsEnum colorEnum)
        //{
        //    switch (colorEnum)
        //    {
        //        case ColorsEnum.None:
        //            return Color.black;
        //        case ColorsEnum.Red:
        //            return new Color(0.55f, 0.06f, 0.02f);
        //        case ColorsEnum.Orange:
        //            return new Color(0.85f, 0.56f, 0.03f);
        //        case ColorsEnum.Yellow:
        //            return new Color(0.93f, 0.69f, 0.4f);
        //        case ColorsEnum.Green:
        //            return Color.green;
        //        case ColorsEnum.Blue:
        //            return new Color(0.12f, 0.46f, 0.69f);
        //        case ColorsEnum.Cyan:
        //            return Color.cyan;
        //        case ColorsEnum.Purple:
        //            return new Color(0.5f, 0f, 0.5f);
        //        case ColorsEnum.White:
        //            return Color.white;
        //        case ColorsEnum.Black:
        //            return Color.black;
        //        case ColorsEnum.Brown:
        //            return new Color(0.62f, 0.33f, 0.07f);
        //        case ColorsEnum.Magenta:
        //            return Color.magenta;
        //        case ColorsEnum.Tan:
        //            return new Color(0.88f, 0.75f, 0.58f);
        //        case ColorsEnum.Olive:
        //            return new Color(0.45f, 0.55f, 0.37f);
        //        case ColorsEnum.Maroon:
        //            return new Color(0.49f, 0f, 0f);
        //        case ColorsEnum.Navy:
        //            return new Color(0f, 0f, 0.49f);
        //        case ColorsEnum.Aquamarine:
        //            return new Color(0f, 0.96f, 0.72f);
        //        case ColorsEnum.Turquoise:
        //            return new Color(0.08f, 0.41f, 0.54f);
        //        case ColorsEnum.Silver:
        //            return new Color(0.77f, 0.77f, 0.77f);
        //        case ColorsEnum.Lime:
        //            return new Color(0.39f, 0.67f, 0.46f);
        //        case ColorsEnum.Teal:
        //            return new Color(0f, 0.49f, 0.49f);
        //        case ColorsEnum.Indigo:
        //            return new Color(0.2f, 0.07f, 0.53f);
        //        case ColorsEnum.Violet:
        //            return new Color(0.56f, 0.33f, 0.62f);
        //        case ColorsEnum.Pink:
        //            return new Color(0.95f, 0.49f, 0.44f);
        //        case ColorsEnum.Gray:
        //            return new Color(0.78f, 0.87f, 0.9f);
        //        case ColorsEnum.LightBlue:
        //            return new Color(0.05f, 0.74f, 0.98f);
        //        case ColorsEnum.DarkBlue:
        //            return new Color(0.17f, 0.29f, 0.55f);
        //        case ColorsEnum.LightOrange:
        //            return new Color(0.98f, 0.56f, 0.24f);
        //        case ColorsEnum.DarkOrange:
        //            return new Color(186 / Max, 95 / Max, 0 / Max);
        //        case ColorsEnum.LightRed:
        //            return new Color(1f, 0.27f, 0.24f);
        //        case ColorsEnum.DarkRed:
        //            return new Color(255 / Max, 118 / Max, 110 / Max);
        //        case ColorsEnum.LightBrown:
        //            return new Color(0.5f, 0.4f, 0.15f);
        //        case ColorsEnum.DarkBrown:
        //            return new Color(0.17f, 0.07f, 0.03f);
        //        case ColorsEnum.LightGreen:
        //            return new Color(0.79f, 0.75f, 0.25f);
        //        case ColorsEnum.DarkGreen:
        //            return new Color(0.62f, 0.55f, 0.08f);
        //        case ColorsEnum.Beige:
        //            return new Color(0.92f, 0.81f, 0.58f);
        //        case ColorsEnum.Lilac:
        //            return new Color(219 / Max, 112 / Max, 147 / Max); 
        //        case ColorsEnum.LightLilac:
        //            return new Color(230 / Max, 154 / Max, 179 / Max); 
        //        case ColorsEnum.DarkLilac:
        //            return new Color(0.4f, 0.22f, 0.41f); 
        //        case ColorsEnum.Ãinous:
        //            return new Color(0.43f, 0.06f, 0.16f); 
        //        default:
        //            Debug.LogError("NO COLOR");
        //            return new Color(0, 0, 0);
        //        // return new Color(/ 255f, /255f, /255f);
        //    }
        //}
    }
}