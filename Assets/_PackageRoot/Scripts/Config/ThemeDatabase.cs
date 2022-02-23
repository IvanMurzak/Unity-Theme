using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public sealed partial class ThemeDatabase : SerializedScriptableObject
    {
        static          Color               DefaultColor                => Color.white;

        public const    string              PATH                        = "Assets/Resources/Unity-Theme Database.asset";
        public const    string              PATH_FOR_RESOURCES_LOAD     = "Unity-Theme Database";

        public          bool                debug                       = true;

        public          List<ThemeData>     themes                      = new List<ThemeData>();
        public          List<ColorName>     colorNames                  = new List<ColorName>();

        public void AddTheme(string themeName)
        {
            List<ColorData> colors;
            if (themes.Count > 0)
            {
                colors = themes[0].colors.Select(c => new ColorData
                {
                    name = c.name,
                    color = c.color
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

        public bool AddColor(string colorName)
        {
            if (themes.Count > 0)
            {
                if (string.IsNullOrEmpty(colorName))
                    return false;

                if (themes[0].colors.Any(x => x.name == colorName))
                    return false;

                foreach (var theme in themes)
                {
                    theme.colors.Add(new ColorData
                    {
                        name = colorName,
                        color = DefaultColor
                    });
                }
            }

            colorNames.Add(new ColorName
            {
                name = colorName,
                guid = System.Guid.NewGuid().ToString()
            });

            return true;
        }
        public void ChangeColorName(string oldName, string newName)
        {
            var colorName = colorNames.FirstOrDefault(x => x.name == oldName);
            if (colorName != null)
            {
                foreach (var theme in themes)
                {
                    var color = theme.colors.FirstOrDefault(x => x.name == oldName);
                    if (color != null)
                    {
                        color.name = newName;
                    }
                }

                colorName.name = newName;
            }
        }
        public void ChangeThemeName(string oldName, string newName)
        {
            var theme = themes.FirstOrDefault(x => x.themeName == oldName);
            if (theme != null)
            {
                theme.themeName = newName;
            }
        }

        public void SortColors()
        {
            foreach (var theme in themes)
            {
                theme.colors.Sort(ColorData.Compare);
            }
        }

        public void RemoveTheme(ThemeData theme) => themes.Remove(theme);
        public void RemoveColor(string colorName)
        {
            var nameToRemove = colorNames.FirstOrDefault(x => x.name == colorName);
            if (nameToRemove != null)
                colorNames.Remove(nameToRemove);

            foreach (var theme in themes)
            {
                var toRemove = theme.colors.FirstOrDefault(x => x.name == colorName);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
            }
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}