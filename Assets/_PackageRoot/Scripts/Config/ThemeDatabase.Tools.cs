using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class ThemeDatabase : ScriptableObject
    {                           
               static       Color               DefaultColor                => Color.white;
        public delegate     void                OnTheme                     (ThemeData theme);
        public delegate     void                OnColor                     (ThemeData theme, ColorData color);

        public const        string              PATH                        = "Assets/Resources/Unity-Theme Database.asset";
        public const        string              PATH_FOR_RESOURCES_LOAD     = "Unity-Theme Database";

        public              OnTheme             onThemeChanged;
        public              OnColor             onThemeColorChanged;

        private void OnValidate()
        {
            if (colors == null) colors = new List<ColorDataRef>();
            if (themes == null) themes = new List<ThemeData>();

            if (currentThemeIndex < 0) currentThemeIndex = 0;
            if (currentThemeIndex >= themes.Count) currentThemeIndex = Mathf.Max(0, themes.Count - 1);

            var changed = false;
            foreach (var theme in themes)
            {
                changed |= RemoveLegacyColors(theme);
                changed |= AddMissedColors(theme);
                changed |= SortColors(theme);
            }

            if (changed && CurrentTheme != null)
            {
                onThemeChanged?.Invoke(CurrentTheme);
            }
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
        private bool SortColors(ThemeData theme)
        {
            var changed = false;
            for (int i = 0; i < colors.Count; i++)
            {
                if (theme.colors[i].Guid != colors[i].Guid)
                {
                    var colorData = theme.colors.First(colorData => colorData.Guid == colors[i].Guid);
                    var colorIndex = theme.colors.IndexOf(colorData);
                    
                    var temp = theme.colors[i];
                    theme.colors[i] = theme.colors[colorIndex];
                    theme.colors[colorIndex] = temp;

                    changed = true;
                }
            }
            return changed;
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}