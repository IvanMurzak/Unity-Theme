using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract partial class BaseColorBinder : LogableMonoBehaviour
    {
        [SerializeField] protected ColorBinderData data = new ColorBinderData();

        /// <summary>
        /// List of objects that should be marked as dirty when color is changed.
        /// This is useful when editing prefabs in the editor.
        /// </summary>
        protected virtual IEnumerable<Object> ColorTargets { get; } = null;
        protected bool IsSubscribed { get; private set; }

        protected virtual void Awake()
        {
            data = data ?? new ColorBinderData();
            if (string.IsNullOrEmpty(data.colorGuid) && string.IsNullOrEmpty(data.ColorName))
                data.colorGuid = Theme.Instance?.GetColorFirst().Guid;

            if (!data.IsConnected)
                LogError("Color with GUID='{0}' not found in database", data.colorGuid);
        }

        protected virtual void OnEnable() => Enable();
        protected virtual void Enable()
        {
            InvalidateColor(Theme.Instance.CurrentTheme);
            Subscribe();
        }
        protected virtual void OnDisable() => Unsubscribe();
        protected virtual void Subscribe()
        {
            if (IsSubscribed)
                return;
            LogTrace("Subscribing");
            Theme.Instance.onThemeChanged += InvalidateColor;
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
            Theme.Instance.onThemeChanged -= InvalidateColor;
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
        protected virtual void InvalidateColor(ThemeData theme)
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

                LogTrace("Invalidating color");
                if (theme == null)
                {
                    LogError("Current theme is null, can't invalidate color");
                    return;
                }

                var colorData = theme.GetColorByGuid(data.colorGuid);
                if (colorData == null)
                {
                    LogError("Color with GUID='{0}' not found in database", data.colorGuid);
                }
                else
                {
                    if (!CanApplyColor())
                    {
                        LogWarning("Can't apply color");
                        return;
                    }
                    var targetColor = GetTargetColor(colorData);
                    var currentColor = GetColor();
                    if (currentColor.HasValue && currentColor.Value == targetColor)
                        return; // skip if color is the same

                    if (InternalSetColor(targetColor))
                        SetDirty();
                }
            }
            catch (System.Exception e)
            {
                if (Theme.IsLogActive(DebugLevel.Exception))
                    Debug.LogException(e);
            }
        }

        protected virtual Color GetTargetColor(ColorData colorData)
        {
            var result = colorData.Color;

            if (data.overrideAlpha)
                result.a = data.alpha;

            return result;
        }
        protected virtual bool InternalSetColor(Color color)
        {
            if (Theme.IsLogActive(DebugLevel.Log))
                Log("Setting color '<b>{0}</b>' to {1}", data.ColorName, color.ToHexRGBA());

            SetColorInternal(color);
            return true;
        }
        protected virtual bool CanApplyColor() => true;
        protected virtual bool SetColorInternal(ColorData colorData)
        {
            if (colorData == null)
            {
                LogError("Color is null. Can't set it as a color for the binder");
                return false;
            }
            if (data.colorGuid == colorData.Guid)
                return true; // skip if the same color

            data.colorGuid = colorData.Guid;
            var color = GetTargetColor(colorData);

            if (Theme.IsLogActive(DebugLevel.Log))
                Log("Setting color '<b>{0}</b>' to {1}", data.ColorName, color.ToHexRGBA());

            SetColorInternal(color);
            SetDirty();
            return true;
        }
        protected abstract void SetColorInternal(Color color);

        /// <summary>
        /// Get color from target
        /// </summary>
        /// <returns>Returns nullable color</returns>
        public abstract Color? GetColor();

        private void SetDirty(Object obj)
        {
#if UNITY_EDITOR
            if (obj.IsNull())
                return;
            UnityEditor.EditorUtility.SetDirty(obj);
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(obj);
#endif
        }
        private void OnThemeColorChanged(ThemeData themeData, ColorData colorData) => InvalidateColor(themeData);
    }
}