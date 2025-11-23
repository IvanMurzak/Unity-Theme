using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract partial class BaseMultiColorBinder : MonoBehaviour
    {
        [SerializeField] protected MultiColorBinderEntry[] colorEntries = new MultiColorBinderEntry[0];

        /// <summary>
        /// List of objects that should be marked as dirty when color is changed.
        /// This is useful when editing prefabs in the editor.
        /// </summary>
        protected virtual IEnumerable<Object> ColorTargets { get; } = null;
        protected bool IsSubscribed { get; private set; }

        protected virtual void Awake()
        {
            colorEntries = colorEntries ?? new MultiColorBinderEntry[0];

            // Initialize any null entries
            for (int i = 0; i < colorEntries.Length; i++)
            {
                if (colorEntries[i] == null)
                    colorEntries[i] = new MultiColorBinderEntry();
                if (colorEntries[i].colorData == null)
                    colorEntries[i].colorData = new ColorBinderData();

                // Set default color GUID if not set
                if (string.IsNullOrEmpty(colorEntries[i].colorData.colorGuid) &&
                    string.IsNullOrEmpty(colorEntries[i].colorData.ColorName))
                {
                    colorEntries[i].colorData.colorGuid = Theme.Instance?.GetColorFirst().Guid;
                }
            }

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
            if (Theme.IsLogActive(DebugLevel.Trace) && this.IsNotNull())
                Debug.Log($"[Theme] Subscribing multi-color binder at <b>{GameObjectPath()}</b>", gameObject);
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
            if (Theme.IsLogActive(DebugLevel.Trace) && this.IsNotNull())
                Debug.Log($"[Theme] Unsubscribing multi-color binder at <b>{GameObjectPath()}</b>", gameObject);
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
            if (Theme.IsLogActive(DebugLevel.Trace) && this.IsNotNull())
                Debug.Log($"[Theme] SetDirty at <b>{GameObjectPath()}</b>", gameObject);

            SetDirty(this);
            if (ColorTargets != null)
            {
                foreach (var target in ColorTargets)
                    SetDirty(target);
            }
#endif
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
                if (Theme.IsLogActive(DebugLevel.Trace) && this.IsNotNull())
                    Debug.Log($"[Theme] Invalidating colors at <b>{GameObjectPath()}</b>", gameObject);
                if (theme == null)
                {
                    if (Theme.IsLogActive(DebugLevel.Error))
                        Debug.LogError($"[Theme] Current theme is null, can't invalidate colors at <b>{GameObjectPath()}</b>", gameObject);
                    return;
                }

                if (!CanApplyColors())
                {
                    if (Theme.IsLogActive(DebugLevel.Warning) && this.IsNotNull())
                        Debug.LogWarning($"[Theme] Can't apply colors at <b>{GameObjectPath()}</b>", gameObject);
                    return;
                }

                var targetColors = GetTargetColors(theme);
                var currentColors = GetColors();

                // Check if colors have changed
                bool hasChanged = false;
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
                    if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                        Debug.LogError($"[Theme] Color with GUID='{entry.colorData.colorGuid}' not found for entry '{entry.label}' at <b>{GameObjectPath()}</b>", gameObject);
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
            if (Theme.IsLogActive(DebugLevel.Log) && this.IsNotNull())
            {
                var colorNames = string.Join(", ", System.Array.ConvertAll(colorEntries, e => $"'{e.label}'"));
                Debug.Log($"[Theme] SetColors {colorNames} at <b>{GameObjectPath()}</b>", gameObject);
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
                {
                    if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                        Debug.LogError($"[Theme] Color with GUID='{entry.colorData.colorGuid}' for entry '{entry.label}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
                }
            }
        }

        protected abstract void SetColorsInternal(Color[] colors);

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
        protected string GameObjectPath() => Extensions.GameObjectPath(this);
    }
}
