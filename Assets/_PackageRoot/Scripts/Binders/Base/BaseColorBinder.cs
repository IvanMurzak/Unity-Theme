using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract partial class BaseColorBinder : MonoBehaviour
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
            {
                if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                    Debug.LogError($"[Theme] Color with GUID='{data.colorGuid}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
            }
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
            if (Theme.IsLogActive(DebugLevel.Trace) && this.IsNotNull())
                Debug.Log($"[Theme] Subscribing at <b>{GameObjectPath()}</b>", gameObject);
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
            if (Theme.IsLogActive(DebugLevel.Trace) && this.IsNotNull())
                Debug.Log($"[Theme] Unsubscribing at <b>{GameObjectPath()}</b>", gameObject);
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
                if (Theme.IsLogActive(DebugLevel.Trace) && this.IsNotNull())
                    Debug.Log($"[Theme] Invalidating color at <b>{GameObjectPath()}</b>", gameObject);
                if (theme == null)
                {
                    if (Theme.IsLogActive(DebugLevel.Error))
                        Debug.LogError($"[Theme] Current theme is null, can't invalidate color at <b>{GameObjectPath()}</b>", gameObject);
                    return;
                }

                var colorData = theme.GetColorByGuid(data.colorGuid);
                if (colorData == null)
                {
                    if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                        Debug.LogError($"[Theme] Color with GUID='{data.colorGuid}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
                }
                else
                {
                    if (!CanApplyColor())
                    {
                        if (Theme.IsLogActive(DebugLevel.Warning) && this.IsNotNull())
                            Debug.LogWarning($"[Theme] Can't apply color at <b>{GameObjectPath()}</b>", gameObject);
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
            if (Theme.IsLogActive(DebugLevel.Log) && this.IsNotNull())
                Debug.Log($"[Theme] SetColor '<b>{data.ColorName}</b>' {color.ToHexRGBA()} at <b>{GameObjectPath()}</b>", gameObject);
            SetColorInternal(color);
            return true;
        }
        protected virtual bool CanApplyColor() => true;
        protected virtual bool SetColorInternal(ColorData colorData)
        {
            if (colorData == null)
            {
                if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                    Debug.LogError($"[Theme] Color is null. Can't set it as a color for the binder", gameObject);
                return false;
            }
            if (data.colorGuid == colorData.Guid)
                return true; // skip if the same color

            data.colorGuid = colorData.Guid;
            var color = GetTargetColor(colorData);

            if (Theme.IsLogActive(DebugLevel.Log) && this.IsNotNull())
                Debug.Log($"[Theme] SetColor '<b>{data.ColorName}</b>' {color.ToHexRGBA()} at <b>{GameObjectPath()}</b>", gameObject);

            SetColorInternal(color);
            SetDirty();
            return true;
        }
        protected abstract void SetColorInternal(Color color);
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
        protected string GameObjectPath() => Extensions.GameObjectPath(this);
    }
}