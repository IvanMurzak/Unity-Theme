using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public sealed partial class ThemeDatabase : SerializedScriptableObject
    {
        [BoxGroup("B", false), HorizontalGroup("B/H")]
        [TitleGroup("B/H/Settings"), PropertyOrder(-10)]                            
        public bool debug = true;
                                    
        [TitleGroup("B/H/Settings"), ValueDropdown("ThemeNames")]
        [LabelText("Current Theme"), PropertyOrder(-9), ShowInInspector]           
        public string CurrentThemeName
        {
            get => CurrentTheme?.themeName;
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


        [PropertyOrder(-8), PropertySpace]
        [ListDrawerSettings(DraggableItems = true, Expanded = false, NumberOfItemsPerPage = 20, ShowItemCount = false, HideAddButton = false, HideRemoveButton = true)]
        [TableList(AlwaysExpanded = false, NumberOfItemsPerPage = 20, IsReadOnly = true, HideToolbar = false, ShowPaging = true)]
        [SerializeField, HideReferenceObjectPicker]
        List<ColorDataRef> colors = new List<ColorDataRef>();

        [GUIColor(0.8f, 1.0f, 0.8f, 1)]
        [PropertyOrder(-7), LabelText("ADD COLOR")]
        [ButtonGroup("ColorButtons"), Button(ButtonSizes.Medium, Style = ButtonStyle.Box)]
        void AddColor() => AddColor("New Color", DefaultColor);

        [GUIColor(0.8f, 1.0f, 0.8f, 1)]
        [PropertyOrder(-6), LabelText("SORT")]
        [ButtonGroup("ColorButtons"), Button(ButtonSizes.Medium, Style = ButtonStyle.Box)]
        void SortColors()
        {
            foreach (var theme in themes)
            {
                theme.colors.Sort(ColorData.Compare);
            }
        }

        [SerializeField, HideReferenceObjectPicker, PropertySpace]
        [ListDrawerSettings(DraggableItems = false, Expanded = true, NumberOfItemsPerPage = 20, ShowItemCount = false, HideAddButton = false, HideRemoveButton = true)]
        List<ThemeData> themes = new List<ThemeData>();

        private void OnValidate()
        {
            if (colors == null) colors = new List<ColorDataRef>();
            if (themes == null) themes = new List<ThemeData>();

            if (currentThemeIndex < 0) currentThemeIndex = 0;
            if (currentThemeIndex >= themes.Count) currentThemeIndex = Mathf.Max(0, themes.Count - 1);

            foreach (var theme in themes)
            {
                RemoveLegacyColors(theme);
                AddMissedColors(theme);
                UpdateColorNames(theme);
                SortColors(theme);
            }

            if (CurrentTheme != null)
            {
                onThemeChanged?.Invoke(CurrentTheme);
            }
        }
        private void RemoveLegacyColors(ThemeData theme)
        {
            theme.colors.RemoveAll(colorData => colors.All(colorRef => colorRef.guid != colorData.guid));
        }
        private void AddMissedColors(ThemeData theme)
        {
            foreach (var colorRef in colors)
            {
                if (theme.colors.All(colorData => colorData.guid != colorRef.guid))
                {
                    theme.colors.Add(new ColorData
                    {
                        guid = colorRef.guid,
                        name = colorRef.name,
                        color = DefaultColor
                    });
                }
            }
        }
        private void UpdateColorNames(ThemeData theme)
        {
            foreach (var colorData in theme.colors)
            {
                var colorRef = colors.First(x => x.guid == colorData.guid);
                colorData.name = colorRef.name;
            }
        }
        private void SortColors(ThemeData theme)
        {
            for (int i = 0; i < colors.Count; i++)
            {
                if (theme.colors[i].guid != colors[i].guid)
                {
                    var colorData = theme.colors.First(colorData => colorData.guid == colors[i].guid);
                    var colorIndex = theme.colors.IndexOf(colorData);
                    
                    var temp = theme.colors[i];
                    theme.colors[i] = theme.colors[colorIndex];
                    theme.colors[colorIndex] = temp;
                }
            }
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}