using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract partial class BaseMultiColorBinder : LogableMonoBehaviour
    {
        [SerializeField] protected FixedMultiColorBinderEntries colorEntries = new FixedMultiColorBinderEntries();

        /// <summary>
        /// Defines the labels for each color entry that this binder manages.
        /// Derived classes must implement this to specify their color entry names.
        /// </summary>
        protected abstract string[] ColorEntries { get; }

        /// <summary>
        /// List of objects that should be marked as dirty when color is changed.
        /// This is useful when editing prefabs in the editor.
        /// </summary>
        protected virtual IEnumerable<Object> ColorTargets { get; } = null;
        protected bool IsSubscribed { get; private set; }

        protected virtual void Awake()
        {
            FixColorEntries();
            ValidateColorConnections();
        }

        protected virtual void OnEnable() => Enable();
        protected virtual void Enable()
        {
            InvalidateColors(Theme.Instance.CurrentTheme);
            Subscribe();
        }
        protected virtual void OnDisable() => Unsubscribe();
        protected virtual void Subscribe()
        {
            if (IsSubscribed)
                return;
            LogTrace("Subscribing");
            Theme.Instance.onThemeChanged += InvalidateColors;
            Theme.Instance.onThemeColorChanged += OnThemeColorChanged;
#if UNITY_EDITOR
            if (!Application.isPlaying)
                UnityEditor.EditorApplication.delayCall += Enable;
#endif
            IsSubscribed = true;
        }
        protected virtual void Unsubscribe()
        {
            if (!IsSubscribed)
                return;
            LogTrace("Unsubscribing");
            Theme.Instance.onThemeChanged -= InvalidateColors;
            Theme.Instance.onThemeColorChanged -= OnThemeColorChanged;
#if UNITY_EDITOR
            if (!Application.isPlaying)
                UnityEditor.EditorApplication.delayCall -= Enable;
#endif
            IsSubscribed = false;
        }
        protected virtual void SetDirty()
        {
#if UNITY_EDITOR
            LogTrace("SetDirty");
            SetDirty(this);
            if (ColorTargets != null)
            {
                foreach (var target in ColorTargets)
                    SetDirty(target);
            }
#endif
        }
        protected virtual void FixColorEntries()
        {
            // Get the expected color entries from derived class
            var expectedEntries = ColorEntries;

            // Initialize color entries if not already set or if length doesn't match
            if (colorEntries == null || colorEntries.Length != expectedEntries.Length)
            {
                var entries = new MultiColorBinderEntry[expectedEntries.Length];
                for (int i = 0; i < expectedEntries.Length; i++)
                    entries[i] = new MultiColorBinderEntry(expectedEntries[i]);

                colorEntries = new FixedMultiColorBinderEntries(entries);
            }

            // Initialize any null entries
            for (int i = 0; i < colorEntries.Length; i++)
            {
                if (colorEntries[i] == null)
                    colorEntries[i] = new MultiColorBinderEntry(i < expectedEntries.Length ? expectedEntries[i] : string.Empty);

                if (colorEntries[i].colorData == null)
                    colorEntries[i].colorData = new ColorBinderData();

                // Set default color GUID if not set
                if (string.IsNullOrEmpty(colorEntries[i].colorData.colorGuid) &&
                    string.IsNullOrEmpty(colorEntries[i].colorData.ColorName))
                    colorEntries[i].colorData.colorGuid = Theme.Instance?.GetColorFirst().Guid;
            }
        }
        protected virtual void InvalidateColors(ThemeData theme)
        {
            try
            {
                if (this.IsNull())
                {
                    // `this` is destroyed component. Need to unsubscribe.
                    Unsubscribe();
                    Object.DestroyImmediate(this);
                    return;
                }
                LogTrace("Invalidating colors");

                if (theme == null)
                {
                    LogError("Current theme is null, can't invalidate colors");
                    return;
                }

                if (!CanApplyColors())
                {
                    LogWarning("Can't apply colors, target is not valid");
                    return;
                }

                var targetColors = GetTargetColors(theme);
                var currentColors = GetColors();

                // Check if colors have changed
                var hasChanged = false;
                if (currentColors == null || currentColors.Length != targetColors.Length)
                {
                    hasChanged = true;
                }
                else
                {
                    for (int i = 0; i < targetColors.Length; i++)
                    {
                        if (currentColors[i] != targetColors[i])
                        {
                            hasChanged = true;
                            break;
                        }
                    }
                }

                if (!hasChanged)
                    return; // skip if colors are the same

                if (InternalSetColors(targetColors))
                    SetDirty();
            }
            catch (System.Exception e)
            {
                if (Theme.IsLogActive(DebugLevel.Exception))
                    Debug.LogException(e);
            }
        }

        protected virtual Color[] GetTargetColors(ThemeData theme)
        {
            var result = new Color[colorEntries.Length];

            for (int i = 0; i < colorEntries.Length; i++)
            {
                var entry = colorEntries[i];
                var colorData = theme.GetColorByGuid(entry.colorData.colorGuid);

                if (colorData == null)
                {
                    LogError("Color with GUID='{0}' not found for entry '{1}'", entry.colorData.colorGuid, entry.label);
                    result[i] = Color.magenta; // Error color
                }
                else
                {
                    result[i] = colorData.Color;

                    // Apply alpha override if enabled
                    if (entry.colorData.overrideAlpha)
                        result[i].a = entry.colorData.alpha;
                }
            }

            return result;
        }

        protected virtual bool InternalSetColors(Color[] colors)
        {
            if (Theme.IsLogActive(DebugLevel.Log))
            {
                var colorNames = string.Join(", ", System.Array.ConvertAll(colorEntries.colorBindings, e => $"'{e.label}'"));
                LogTrace($"SetColors {colorNames}");
            }

            if (colors == null || colors.Length != ColorEntries.Length)
            {
                LogError("Invalid colors array. Expected {0} colors for {1} states.", ColorEntries.Length, GetType().Name);
                FixColorEntries();

                // Double check after fixing
                if (colors == null || colors.Length != ColorEntries.Length)
                    return false;

                Log("Fixed color entries length.");
            }

            SetColorsInternal(colors);
            return true;
        }

        protected virtual bool CanApplyColors() => true;

        protected virtual void ValidateColorConnections()
        {
            for (int i = 0; i < colorEntries.Length; i++)
            {
                var entry = colorEntries[i];
                if (entry.colorData == null)
                    continue;

                if (!entry.colorData.IsConnected)
                    LogError("Color with GUID='{0}' for entry '{1}' not found in database", entry.colorData.colorGuid, entry.label);
            }
        }

        protected abstract bool SetColorsInternal(Color[] colors);

        /// <summary>
        /// Get colors from target
        /// </summary>
        /// <returns>Returns array of colors</returns>
        public abstract Color[] GetColors();

        private void SetDirty(Object obj)
        {
#if UNITY_EDITOR
            if (obj.IsNull())
                return;
            UnityEditor.EditorUtility.SetDirty(obj);
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(obj);
#endif
        }
        private void OnThemeColorChanged(ThemeData themeData, ColorData colorData) => InvalidateColors(themeData);
    }
}
