using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme : ScriptableObject
    {        
        public List<ThemeData>     Themes              => themes;
        public IEnumerable<string> ThemeNames          => themes.Select(x => x.themeName);
        public ThemeData           CurrentTheme        => themes.Count == 0 ? null : ((currentThemeIndex >= 0 && currentThemeIndex < themes.Count) ? themes[currentThemeIndex] : null);
        public int                 CurrentThemeIndex
        {
            get => currentThemeIndex;
            set
            {
                if (value >= 0 && value < themes.Count)
                {
                    if (currentThemeIndex != value)
                    {
                        currentThemeIndex = value;
                        NotifyThemeChanged(CurrentTheme);
                    }
                }
            }
        }
        public string              CurrentThemeName
        {
            get => CurrentTheme?.themeName;
            set => CurrentThemeIndex = themes.FindIndex(x => x.themeName == value);
        }

        public ThemeData AddTheme(string themeName, bool setCurrent = false)
        {
            List<ColorData> colors;
            if (themes.Count > 0)
            {
                colors = themes[0].colors
                    .Select(c => new ColorData(c))
                    .ToList();
            }
            else
            {
                colors = new List<ColorData>();
            }
            var theme = new ThemeData(Guid.NewGuid().ToString())
            {
                themeName = themeName,
                colors = colors
            };
            themes.Add(theme);

            if (setCurrent)
                CurrentThemeIndex = themes.Count - 1;
                
            return theme;
        }
        public ThemeData SetOrAddTheme(string themeName, bool setCurrent = false)
        {
            var theme = themes.FirstOrDefault(x => x.themeName == themeName);
            if (theme == null)
                theme = AddTheme(themeName, setCurrent);
            if (setCurrent)
                CurrentThemeIndex = themes.IndexOf(theme);
            return theme;
        }
        public void RemoveTheme(ThemeData theme) => themes.Remove(theme);

        public void RemoveAllThemes()
        {
            themes.Clear();
            currentThemeIndex = -1;
        }

        protected virtual void NotifyThemeChanged(ThemeData theme)
        {
            try
            {
                onThemeChanged?.Invoke(theme);
            }
            catch (Exception e)
            {
                if (debugLevel <= DebugLevel.Exception)
                    Debug.LogException(e);
            }
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}