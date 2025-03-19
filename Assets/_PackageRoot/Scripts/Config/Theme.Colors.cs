using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Theme.Utils;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme
    {
        public IEnumerable<string> ColorNames => colors?.Select(x => x.name);
        public IEnumerable<string> ColorGuids => colors?.Select(x => x.Guid);

        public ColorDataRef GetColorRef(ColorData colorData)            => GetColorRef(colorData.Guid);
        public ColorDataRef GetColorRef(string guid)                    => string.IsNullOrEmpty(guid) ? null : colors.FirstOrDefault(x => x.Guid == guid);

        public string    GetColorGuidByIndex(int index)                 => colors?[index]?.Guid;
        public int       GetColorIndexByGuid(string guid)               => colors?.FindIndex(x => x.Guid == guid) ?? -1;
        public string    GetColorName  (string guid)                    => GetColorRef(guid)?.name;
        public ColorData GetColorByGuid(string guid)                    => GetColorByGuid(guid, CurrentTheme);
        public ColorData GetColorByGuid(string guid, ThemeData theme)   => string.IsNullOrEmpty(guid) ? null : theme?.colors?.FirstOrDefault(x => x.Guid == guid);
        public ColorData GetColorByName(string name)                    => GetColorByName(name, CurrentTheme);
        public ColorData GetColorByName(string name, ThemeData theme)   => string.IsNullOrEmpty(name) ? null : GetColorByGuid(colors.FirstOrDefault(x => x.name == name)?.Guid, theme);

        public ColorData GetColorFirst()                                => GetColorFirst(CurrentTheme);
        public ColorData GetColorFirst(ThemeData theme)                 => theme?.colors?.FirstOrDefault();

        public IReadOnlyList<ColorDataRef> GetColors()                  => colors;
        public ColorDataRef GetColorByIndex(int index)
        {
            if (colors == null)
            {
                if (debugLevel.IsActive(DebugLevel.Error))
                    Debug.LogError($"[Theme] GetColorByIndex error. Colors list is null");
                return null;
            }
            if (index < 0)
            {
                if (debugLevel.IsActive(DebugLevel.Error))
                    Debug.LogError($"[Theme] GetColorByIndex error. Index can't be less than 0");
                return null;
            }
            if (index >= colors.Count)
            {
                if (debugLevel.IsActive(DebugLevel.Error))
                    Debug.LogError($"[Theme] GetColorByIndex error. Index({index}) can't be greater than colors count({colors.Count})");
                return null;
            }
            return colors[index];
        }

        public ColorDataRef AddColor(string colorName) => AddColor(colorName, DefaultColor);
        public ColorDataRef AddColor(string colorName, string colorHex)
        {
            if (!ColorUtility.TryParseHtmlString(colorHex, out var color))
            {
                color = DefaultColor;
                if (debugLevel.IsActive(DebugLevel.Error))
                    Debug.LogError($"[Theme] Color HEX can't be parsed from '{colorHex}'");
            }
            return AddColor(colorName, color);
        }
        public ColorDataRef AddColor(string colorName, Color color)
        {
            var guid = Guid.NewGuid().ToString();
            var colorDataRef = new ColorDataRef(guid, colorName);
            colors.Add(colorDataRef);

            if (themes.Count > 0)
            {
                if (string.IsNullOrEmpty(colorName))
                    return colorDataRef;

                if (themes[0].colors.Any(x => x.Guid == guid))
                    return colorDataRef;

                foreach (var theme in themes)
                {
                    theme.colors.Add(new ColorData(guid, color));
                }
            }
            return colorDataRef;
        }
        public ColorData SetColor(string colorName, string colorHex)
        {
            if (!ColorUtility.TryParseHtmlString(colorHex, out var color))
            {
                color = DefaultColor;
                if (debugLevel.IsActive(DebugLevel.Error))
                    Debug.LogError($"[Theme] Color HEX can't be parsed from '{colorHex}'");
            }
            return SetColor(colorName, color);
        }
        public ColorData SetColor(string colorName, Color color)
        {
            var colorData = GetColorByName(colorName);
            if (colorData == null)
            {
                if (debugLevel.IsActive(DebugLevel.Error))
                    Debug.LogError($"[Theme] SetColor error. Color with name '{colorName}' not found");
                return null;
            }
            colorData.Color = color;
            NotifyColorChanged(colorData);
            return colorData;
        }
        public ColorData SetOrAddColor(string colorName) => SetOrAddColor(colorName, DefaultColor);
        public ColorData SetOrAddColor(string colorName, string colorHex)
        {
            if (!ColorUtility.TryParseHtmlString(colorHex, out var color))
            {
                color = DefaultColor;
                if (debugLevel.IsActive(DebugLevel.Error))
                    Debug.LogError($"[Theme] Color HEX can't be parsed from '{colorHex}'");
            }
            return SetOrAddColor(colorName, color);
        }
        public ColorData SetOrAddColor(string colorName, Color color)
        {
            var colorData = GetColorByName(colorName);
            if (colorData == null)
            {
                AddColor(colorName, color);
                colorData = GetColorByName(colorName);
            }
            colorData.Color = color;
            NotifyColorChanged(colorData);
            return colorData;
        }

        public bool RemoveColor(ColorData color)
        {
            foreach (var theme in themes)
            {
                theme.colors.RemoveAll(x => x.Guid == color.Guid);
            }
            var refToRemove = colors.FirstOrDefault(x => x.Guid == color.Guid);
            if (refToRemove != null)
            {
                var result = colors.Remove(refToRemove);
                if (result)
                    NotifyColorChanged(color);
                return result;
            }
            return false;
        }
        public bool RemoveColor(ColorDataRef colorRef)
        {
            var color = CurrentTheme.colors.FirstOrDefault(x => x.Guid == colorRef.Guid);
            foreach (var theme in themes)
            {
                theme.colors.RemoveAll(x => x.Guid == colorRef.Guid);
            }
            var result = colors.Remove(colorRef);
            if (result)
                NotifyColorChanged(color);
            return result;
        }
        public bool RemoveColorByName(string name)
        {
            var colorRef = colors.FirstOrDefault(x => x.name == name);
            if (colorRef == null)
            {
                if (debugLevel.IsActive(DebugLevel.Warning))
                    Debug.LogWarning($"[Theme] Can't RemoveColorByName(`{name}`), because it doesn't exist");
                return false;
            }
            return RemoveColor(colorRef);
        }
        public void UpdateColor(ThemeData theme, string guid, Color color)
        {
            var index = theme.colors.FindIndex(x => x.Guid == guid);
            if (index >= 0)
            {
                theme.colors[index].Color = color;
                if (CurrentTheme == theme)
                    NotifyColorChanged(theme.colors[index], theme);
            }
        }

        public void RemoveAllColors()
        {
            colors.Clear();
            foreach (var theme in themes)
                theme.colors.Clear();
        }
        public void SortColorsByName()
        {
            colors.Sort((l, r) => ColorDataRef.CompareByName(l, r));
        }

        protected virtual void NotifyColorChanged(ColorData colorData, ThemeData theme = null)
            => Safe.Run(onThemeColorChanged, theme ??= CurrentTheme, colorData, logLevel: debugLevel);
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}