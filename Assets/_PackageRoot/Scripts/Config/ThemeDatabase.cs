using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public sealed partial class ThemeDatabase : SerializedScriptableObject
    {
               static       Color               DefaultColor                => Color.white;
        public delegate     void                OnTheme                     (ThemeData theme);

        public const        string              PATH                        = "Assets/Resources/Unity-Theme Database.asset";
        public const        string              PATH_FOR_RESOURCES_LOAD     = "Unity-Theme Database";

        [HideInInspector]
        public              OnTheme             onThemeChanged;
        public              string[]            ThemeNames                  => themes.Select(x => x.themeName).ToArray();
        public              ThemeData           CurrentTheme                => themes.Count == 0 ? null : ((currentThemeIndex >= 0 && currentThemeIndex < themes.Count) ? themes[currentThemeIndex] : null);
        public              List<string>        ColorNames                  => colors?.Select(x => x.name)?.ToList();
        public              List<string>        ColorGuids                  => colors?.Select(x => x.guid)?.ToList();

        [SerializeField, HideInInspector]
                            int                 currentThemeIndex           = 0;
        [HideInInspector]
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

        public ColorData GetColorByGuid(string guid)                    => GetColorByGuid(guid, ThemeDatabaseInitializer.Config.CurrentTheme);
        public ColorData GetColorByGuid(string guid, ThemeData theme)   => string.IsNullOrEmpty(guid) ? null : theme?.colors?.FirstOrDefault(x => x.guid == guid);
        public ColorData GetColorByName(string name)                    => GetColorByName(name, ThemeDatabaseInitializer.Config.CurrentTheme);
        public ColorData GetColorByName(string name, ThemeData theme)   => string.IsNullOrEmpty(name) ? null : GetColorByGuid(colors.FirstOrDefault(x => x.name == name)?.guid, theme);

        public ColorData GetColorFirst() => GetColorFirst(ThemeDatabaseInitializer.Config.CurrentTheme);
        public ColorData GetColorFirst(ThemeData theme) => theme?.colors?.FirstOrDefault();

        public void AddTheme(string themeName)
        {
            List<ColorData> colors;
            if (themes.Count > 0)
            {
                colors = themes[0].colors.Select(c => new ColorData
                {
                    guid    = c.guid,
                    name    = c.name,
                    color   = c.color
                }).ToList();
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
        public void AddColor(string colorName, string colorHex)
        {
            var color = DefaultColor;
            if (!ColorUtility.TryParseHtmlString(colorHex, out color))
            {
                Debug.LogError($"Color HEX can't be parsed from '{colorHex}'");
            }
            AddColor(colorName, color);
        }
        public void AddColor(string colorName, Color color)
        {
            var guid = System.Guid.NewGuid().ToString();
            colors.Add(new ColorDataRef
            {
                guid = guid,
                name = colorName
            });

            if (themes.Count > 0)
            {
                if (string.IsNullOrEmpty(colorName))
                    return;

                if (themes[0].colors.Any(x => x.guid == guid))
                    return;

                foreach (var theme in themes)
                {
                    theme.colors.Add(new ColorData
                    {
                        guid    = guid,
                        name    = colorName,
                        color   = color
                    });
                }
            }
        }

        public void RemoveTheme(ThemeData theme) => themes.Remove(theme);
        public void RemoveColor(ColorData colorData)
        {
            foreach (var theme in themes)
            {
                var toRemove = theme.colors.FirstOrDefault(x => x.guid == colorData.guid);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
            }
            var refToRemove = colors.FirstOrDefault(x => x.guid == colorData.guid);
            if (refToRemove != null) 
                colors.Remove(refToRemove);
        }
        public void RemoveColor(ColorDataRef colorRef)
        {
            foreach (var theme in themes)
            {
                var toRemove = theme.colors.FirstOrDefault(x => x.guid == colorRef.guid);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
            }
            if (colorRef != null)
                colors.Remove(colorRef);
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}