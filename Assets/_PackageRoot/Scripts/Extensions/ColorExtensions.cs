using UnityEngine;

namespace Unity.Theme
{
    public static class ColorExtensions
    {
        public static string ToHexRGB(this Color color, bool hexSymbol = true) => hexSymbol
            ? $"#{ColorUtility.ToHtmlStringRGB(color)}"
            : ColorUtility.ToHtmlStringRGB(color);

        public static string ToHexRGBA(this Color color, bool hexSymbol = true) => hexSymbol
            ? $"#{ColorUtility.ToHtmlStringRGBA(color)}"
            : ColorUtility.ToHtmlStringRGBA(color);

        public static Color SetR(this Color color, float r)
        {
            color.r = r;
            return color;
        }
        public static Color SetG(this Color color, float g)
        {
            color.g = g;
            return color;
        }
        public static Color SetB(this Color color, float b)
        {
            color.b = b;
            return color;
        }
        public static Color SetA(this Color color, float a)
        {
            color.a = a;
            return color;
        }
    }
}