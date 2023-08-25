using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme : ScriptableObject
    {        
        public IEnumerable<string> ColorNames => colors?.Select(x => x.name);
        public IEnumerable<string> ColorGuids => colors?.Select(x => x.Guid);
        
        public ColorDataRef GetColorRef(ColorData colorData)            => GetColorRef(colorData.Guid);
        public ColorDataRef GetColorRef(string guid)                    => string.IsNullOrEmpty(guid) ? null : colors.FirstOrDefault(x => x.Guid == guid);
        
        public string    GetColorName  (string guid)                    => GetColorRef(guid)?.name;
        public ColorData GetColorByGuid(string guid)                    => GetColorByGuid(guid, CurrentTheme);
        public ColorData GetColorByGuid(string guid, ThemeData theme)   => string.IsNullOrEmpty(guid) ? null : theme?.colors?.FirstOrDefault(x => x.Guid == guid);
        public ColorData GetColorByName(string name)                    => GetColorByName(name, CurrentTheme);
        public ColorData GetColorByName(string name, ThemeData theme)   => string.IsNullOrEmpty(name) ? null : GetColorByGuid(colors.FirstOrDefault(x => x.name == name)?.Guid, theme);

        public ColorData GetColorFirst()                                => GetColorFirst(CurrentTheme);
        public ColorData GetColorFirst(ThemeData theme)                 => theme?.colors?.FirstOrDefault();

        public ColorDataRef AddColor(string colorName) => AddColor(colorName, DefaultColor);
        public ColorDataRef AddColor(string colorName, string colorHex)
        {
            var color = DefaultColor;
            if (!ColorUtility.TryParseHtmlString(colorHex, out color))
            {
                if (debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color HEX can't be parsed from '{colorHex}'");
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
            var color = DefaultColor;
            if (!ColorUtility.TryParseHtmlString(colorHex, out color))
            {
                if (debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color HEX can't be parsed from '{colorHex}'");
            }
            return SetColor(colorName, color);
        }
        public ColorData SetColor(string colorName, Color color)
        {
            var colorData = GetColorByName(colorName);
            if (colorData == null)
            {
                if (debugLevel <= DebugLevel.Error)
                    Debug.LogError($"SetColor error. Color with name '{colorName}' not found");
                return null;
            }
            colorData.color = color;
            return colorData;
        }
        public ColorData SetOrAddColor(string colorName, string colorHex)
        {
            var color = DefaultColor;
            if (!ColorUtility.TryParseHtmlString(colorHex, out color))
            {
                if (debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color HEX can't be parsed from '{colorHex}'");
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
            colorData.color = color;
            NotifyColorChanged(colorData);
            return colorData;
        }

        public bool RemoveColor(ColorData color)
        {
            foreach (var theme in themes)
            {
                var toRemove = theme.colors.FirstOrDefault(x => x.Guid == color.Guid);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
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
                var toRemove = theme.colors.FirstOrDefault(x => x.Guid == colorRef.Guid);
                if (toRemove != null)
                    theme.colors.Remove(toRemove);
            }
            var result = colors.Remove(colorRef);
            if (result)
                NotifyColorChanged(color);
            return result;
        }
        public void UpdateColor(ThemeData theme, ColorData color)
        {
            var index = theme.colors.FindIndex(x => x.Guid == color.Guid);
            if (index >= 0)
            {
                theme.colors[index] = color;
                if (CurrentTheme == theme)
                    NotifyColorChanged(color);
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
            foreach (var theme in themes)
                theme.colors.Sort((l, r) => 
                {
                    var refL = colors.FirstOrDefault(x => x.Guid == l.Guid);
                    var refR = colors.FirstOrDefault(x => x.Guid == r.Guid);
                    return ColorDataRef.Compare(refL, refR);
                });
        }

        protected virtual void NotifyColorChanged(ColorData colorData, ThemeData theme = null)
        {
            theme ??= CurrentTheme;
            try
            {
                onThemeColorChanged?.Invoke(CurrentTheme, colorData);
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