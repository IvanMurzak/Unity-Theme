using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme
    {
        public List<ThemeData> Themes => themes;
        public IEnumerable<string> ThemeNames => themes.Select(x => x.themeName);
        public ThemeData CurrentTheme
        {
            get
            {
                if ((themes?.Count ?? 0) == 0)
                    return null;

                if (currentThemeIndex < themes.Count)
                    return themes[currentThemeIndex];

                CurrentThemeIndex = themes.Count - 1;
                return themes[currentThemeIndex];
            }
        }

        public int CurrentThemeIndex
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
        public string CurrentThemeName
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
            var theme = themes.FirstOrDefault(x => x.themeName == themeName);
            if (theme == null)
            {
                theme = AddTheme(themeName, setCurrent);
                return theme;
            }
            if (setCurrent)
                CurrentThemeIndex = themes.IndexOf(theme);
            return theme;
        }
        public bool RemoveTheme(ThemeData theme)
        {
            var currentTheme = CurrentTheme;
            var result = themes.Remove(theme);
            if (result && currentTheme == theme)
                NotifyThemeChanged(CurrentTheme);
            return result;
        }
        public int RemoveTheme(string themeName)
        {
            var currentTheme = CurrentTheme;
            var result = themes.RemoveAll(x => x.themeName == themeName);
            if (result > 0 && currentTheme != CurrentTheme)
                NotifyThemeChanged(CurrentTheme);
            return result;
        }

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