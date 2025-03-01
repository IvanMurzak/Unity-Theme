using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    [Serializable]
    public partial class Theme
    {
        public static Color DefaultColor => Color.white;

        public event Action<ThemeData> onThemeChanged;
        public event Action<ThemeData, ColorData>  onThemeColorChanged;

        public void OnValidate()
        {
            colors ??= new List<ColorDataRef>();
            themes ??= new List<ThemeData>();

            currentThemeIndex = Mathf.Clamp(currentThemeIndex, 0, themes.Count - 1);

            var changed = CreateThemeGuid(themes);
            foreach (var theme in themes)
            {
                changed |= RemoveLegacyColors(theme);
                changed |= AddMissedColors(theme);
            }

            if (changed && CurrentTheme != null)
                NotifyThemeChanged(CurrentTheme);
        }
        private bool CreateThemeGuid(List<ThemeData> themes)
        {
            var changed = false;
            for (var i = 0; i < themes.Count; i++)
            {
                if (string.IsNullOrEmpty(themes[i].Guid))
                {
                    var guid = System.Guid.NewGuid().ToString();
                    themes[i] = new ThemeData(guid)
                    {
                        expanded = themes[i].expanded,
                        themeName = themes[i].themeName,
                        colors = themes[i].colors
                    };
                    changed = true;
                }
            }
            return changed;
        }
        private bool RemoveLegacyColors(ThemeData theme)
        {
            return theme.colors.RemoveAll(colorData => colors.All(colorRef => colorRef.Guid != colorData.Guid)) > 0;
        }
        private bool AddMissedColors(ThemeData theme)
        {
            var changed = false;
            foreach (var colorRef in colors)
            {
                if (theme.colors.All(colorData => colorData.Guid != colorRef.Guid))
                {
                    theme.colors.Add(new ColorData(colorRef, DefaultColor));
                    changed = true;
                }
            }
            return changed;
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}