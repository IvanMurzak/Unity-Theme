using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme
    {        
        public List<ThemeData>     Themes              => themes;
        public IEnumerable<string> ThemeNames          => themes.Select(x => x.themeName);
        public ThemeData           CurrentTheme        => themes.Count > 0 
            ? ((currentThemeIndex >= 0 && currentThemeIndex < themes.Count)
                ? themes[currentThemeIndex]
                : null)
            : null;

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
                else if (debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Theme index {value} is out of range");
            }
        }
        public string              CurrentThemeName
        {
            get => CurrentTheme?.themeName;
            set => CurrentThemeIndex = themes.FindIndex(x => x.themeName == value);
        }

        public ThemeData AddTheme(string themeName, bool setCurrent = false)
        {
            var colors = themes.Count == 0
                ? new List<ColorData>()
                : themes[0].colors
                    .Select(c => new ColorData(c))
                    .ToList();

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
            var theme = themes.FirstOrDefault(x => x.themeName == themeName) ?? AddTheme(themeName, setCurrent);
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