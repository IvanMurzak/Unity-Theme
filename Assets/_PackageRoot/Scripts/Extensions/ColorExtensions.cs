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
    }
}