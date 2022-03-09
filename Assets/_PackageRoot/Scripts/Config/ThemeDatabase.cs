using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public sealed class ThemeDatabase : SerializedScriptableObject
    {
                                                                static          Color                               DefaultColor                => Color.white;
        [HideInInspector]                                       public delegate void                                OnTheme                     (ThemeData theme);

                                                                public const    string                              PATH                        = "Assets/Resources/Unity-Theme Database.asset";
                                                                public const    string                              PATH_FOR_RESOURCES_LOAD     = "Unity-Theme Database";

        [BoxGroup("B", false), HorizontalGroup("B/H")]
        [TitleGroup("B/H/Settings")]                            public          bool                                debug                       = true;
                                    
        [SerializeField, HideInInspector]                                       int                                 currentThemeIndex           = 0;
        [TitleGroup("B/H/Settings"), ValueDropdown("ThemeNames")]
        [LabelText("Current Theme"), ShowInInspector]           public          string                              CurrentThemeName
        {
            get => CurrentTheme.themeName;
            set
            {
                for (var i = 0; i < themes.Count; i++)
                {
                    if (themes[i].themeName == value)
                    {
                        CurrentThemeIndex = i;
                        break;
                    }
                }
            }
        }

        [HideInInspector]                                       public          int                                 CurrentThemeIndex
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

        [BoxGroup("T", false), HorizontalGroup("T/H")]
        [ListDrawerSettings(DraggableItems = false, Expanded = true, NumberOfItemsPerPage = 20,                     ShowItemCount               = false, HideAddButton = true)]
        [SerializeField, HideReferenceObjectPicker]                             List<ThemeData>                     themes                      = new List<ThemeData>();

        [HideInInspector]                                       public          OnTheme                             onThemeChanged;
        [HideInInspector]                                       public          string[]                            ThemeNames                  => themes.Select(x => x.themeName).ToArray();
        [HideInInspector]                                       public          ThemeData                           CurrentTheme                => themes[currentThemeIndex];
        [HideInInspector]                                       public          List<string>                        ColorNames                  => themes.Count > 0 ? themes[0].colors.Select(x => x.name).ToList() : null;

        [GUIColor(0, 1, 0, 1), PropertySpace]
        [BoxGroup("T", false), ShowInInspector]                                 string                              newColorName;

        [GUIColor(0, 1, 0, 1), PropertySpace]
        [HorizontalGroup("T/B"), Button(ButtonSizes.Medium), LabelText("+ THEME")]
        public void AddTheme()
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
                themeName = "New Theme",
                colors = colors
            });
        }

        [GUIColor(0, 1, 0, 1), PropertySpace]
        [HorizontalGroup("T/B"), Button(ButtonSizes.Medium), LabelText("+ COLOR")]
        public void AddColor()
        {
            if (themes.Count > 0)
            {
                if (string.IsNullOrEmpty(newColorName))
                    return;

                if (themes[0].colors.Any(x => x.name == newColorName))
                    return;

                foreach (var theme in themes)
                {
                    theme.colors.Add(new ColorData
                    {
                        name = newColorName,
                        color = DefaultColor
                    });
                }
            }
        }

        [GUIColor(0, 1, 0, 1), PropertySpace]
        [HorizontalGroup("T/B"), Button(ButtonSizes.Medium), LabelText("SORT COLORS")] 
        void SortColors()
        {
            foreach (var theme in themes)
            {
                theme.colors.Sort(ColorData.Compare);
            }
        }

        public void RemoveTheme(ThemeData theme) => themes.Remove(theme);
        public void RemoveColor(ColorData color)
        {
            foreach (var theme in themes)
            {
                var toRemove = theme.colors.FirstOrDefault(x => x.name == color.name);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
            }
        }

        private void OnValidate()
        {
            if (currentThemeIndex < 0)              currentThemeIndex = 0;
            if (currentThemeIndex >= themes.Count)  currentThemeIndex = Mathf.Max(0, themes.Count - 1);
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}