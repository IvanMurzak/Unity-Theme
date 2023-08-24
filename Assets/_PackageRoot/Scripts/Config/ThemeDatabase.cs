using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class ThemeDatabase : ScriptableObject
    {        
        public              List<ThemeData>     Themes              => themes;
        public              string[]            ThemeNames          => themes.Select(x => x.themeName).ToArray();
        public              ThemeData           CurrentTheme        => themes.Count == 0 ? null : ((currentThemeIndex >= 0 && currentThemeIndex < themes.Count) ? themes[currentThemeIndex] : null);
        public              List<string>        ColorNames          => colors?.Select(x => x.name)?.ToList();
        public              List<string>        ColorGuids          => colors?.Select(x => x.Guid)?.ToList();

        public              int                 CurrentThemeIndex
        {
            get => currentThemeIndex;
            set
            {
                if (value >= 0 && value < themes.Count)
                {
                    if (currentThemeIndex != value)
                    {
                        currentThemeIndex = value;
                        onThemeChanged?.Invoke(CurrentTheme);
                    }
                }
            }
        }
        public              string              CurrentThemeName
        {
            get => CurrentTheme?.themeName;
            set => CurrentThemeIndex = themes.FindIndex(x => x.themeName == value);
        }

        public ColorDataRef GetColorRef(ColorData colorData)            => GetColorRef(colorData.Guid);
        public ColorDataRef GetColorRef(string guid)                    => colors.FirstOrDefault(x => x.Guid == guid);
        public string    GetColorName  (string guid)                    => colors.FirstOrDefault(x => x.Guid == guid)?.name;
        public ColorData GetColorByGuid(string guid)                    => GetColorByGuid(guid, ThemeDatabaseInitializer.Config.CurrentTheme);
        public ColorData GetColorByGuid(string guid, ThemeData theme)   => string.IsNullOrEmpty(guid) ? null : theme?.colors?.FirstOrDefault(x => x.Guid == guid);
        public ColorData GetColorByName(string name)                    => GetColorByName(name, ThemeDatabaseInitializer.Config.CurrentTheme);
        public ColorData GetColorByName(string name, ThemeData theme)   => string.IsNullOrEmpty(name) ? null : GetColorByGuid(colors.FirstOrDefault(x => x.name == name)?.Guid, theme);

        public ColorData GetColorFirst()                                => GetColorFirst(ThemeDatabaseInitializer.Config.CurrentTheme);
        public ColorData GetColorFirst(ThemeData theme)                 => theme?.colors?.FirstOrDefault();

        public void AddTheme(string themeName)
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

            themes.Add(new ThemeData
            {
                themeName = themeName,
                colors = colors
            });
        }
        public void AddColor(string colorName) => AddColor(colorName, DefaultColor);
        public void AddColor(string colorName, string colorHex)
        {
            var color = DefaultColor;
            if (!ColorUtility.TryParseHtmlString(colorHex, out color))
            {
                if (debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color HEX can't be parsed from '{colorHex}'");
            }
            AddColor(colorName, color);
        }
        public void AddColor(string colorName, Color color)
        {
            var guid = System.Guid.NewGuid().ToString();
            colors.Add(new ColorDataRef(guid, colorName));

            if (themes.Count > 0)
            {
                if (string.IsNullOrEmpty(colorName))
                    return;

                if (themes[0].colors.Any(x => x.Guid == guid))
                    return;

                foreach (var theme in themes)
                {
                    theme.colors.Add(new ColorData(guid, color));
                }
            }
        }

        public void RemoveTheme(ThemeData theme) => themes.Remove(theme);
        public void RemoveColor(ColorData color)
        {
            foreach (var theme in themes)
            {
                var toRemove = theme.colors.FirstOrDefault(x => x.Guid == color.Guid);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
            }
            var refToRemove = colors.FirstOrDefault(x => x.Guid == color.Guid);
            if (refToRemove != null) 
                colors.Remove(refToRemove);
        }
        public void RemoveColor(ColorDataRef colorRef)
        {
            foreach (var theme in themes)
            {
                var toRemove = theme.colors.FirstOrDefault(x => x.Guid == colorRef.Guid);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
            }
            if (colorRef != null)
                colors.Remove(colorRef);
        }
        public void UpdateColor(ColorData color)
        {
            
        }
        public void SetColor(ThemeData theme, ColorData color)
        {
            onThemeColorChanged?.Invoke(theme, color);
        }
        public void SortColorsByName()
        {
            foreach (var theme in themes)
                theme.colors.Sort((l, r) => 
                {
                    var refL = colors.FirstOrDefault(x => x.Guid == l.Guid);
                    var refR = colors.FirstOrDefault(x => x.Guid == r.Guid);
                    return ColorDataRef.Compare(refL, refR);
                });
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}