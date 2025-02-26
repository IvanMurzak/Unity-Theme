using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract partial class BaseColorBinder : MonoBehaviour
    {
        [SerializeField] protected ColorBinderData data = new ColorBinderData();

        protected virtual IEnumerable<Object> ColorTargets { get; } = null;

        protected virtual void Awake()
        {
            data = data ?? new ColorBinderData();
            if (string.IsNullOrEmpty(data.colorGuid) && string.IsNullOrEmpty(data.ColorName))
                data.colorGuid = Theme.Instance?.GetColorFirst().Guid;

            if (!data.IsConnected)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color with GUID='{data.colorGuid}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
            }
        }
        protected virtual void Start()
        {
#if UNITY_EDITOR
            TrySetColor(Theme.Instance.CurrentTheme);
#endif
        }

        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Enable();
            }
            else
            {
                UnityEditor.EditorApplication.delayCall += Enable;
            }
#else
            Enable();
#endif
        }
        protected virtual void Enable()
        {
            TrySetColor(Theme.Instance.CurrentTheme);
            Subscribe();
        }
        protected virtual void OnDisable()
        {
            Unsubscribe();
        }
        protected virtual void Subscribe()
        {
            Theme.Instance.onThemeChanged += TrySetColor;
            Theme.Instance.onThemeColorChanged += OnThemeColorChanged;
        }
        protected virtual void Unsubscribe()
        {
            Theme.Instance.onThemeChanged -= TrySetColor;
            Theme.Instance.onThemeColorChanged -= OnThemeColorChanged;
        }
        protected virtual void SetDirty()
        {
            SetDirty(this);
            if (ColorTargets != null)
            {
                foreach (var target in ColorTargets)
                    SetDirty(target);
            }
        }
        protected virtual void TrySetColor(ThemeData theme)
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
                if (theme == null)
                {
                    if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                        Debug.LogError($"Current theme is null at <b>{GameObjectPath()}</b>", gameObject);
                    return;
                }

                var colorData = theme.GetColorByGuid(data.colorGuid);
                if (colorData == null)
                {
                    if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                        Debug.LogError($"Color with GUID='{data.colorGuid}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
                }
                else
                {
                    if (!CanApplyColor())
                    {
                        if (Theme.Instance?.debugLevel <= DebugLevel.Warning)
                            Debug.LogWarning($"Can't apply color at <b>{GameObjectPath()}</b>", gameObject);
                        return;
                    }
                    var targetColor = GetTargetColor(colorData);
                    var currentColor = GetColor();
                    if (targetColor == currentColor)
                        return; // skip if color is the same

                    if (InternalSetColor(targetColor))
                        SetDirty();
                }
            }
            catch (System.Exception e)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Exception)
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
            if (Theme.Instance?.debugLevel <= DebugLevel.Log)
                Debug.Log($"SetColor: '<b>{data.ColorName}</b>' {color.ToHexRGBA()} at <b>{GameObjectPath()}</b>", gameObject);
            SetColor(color);
            return true;
        }
        protected virtual bool CanApplyColor() => true;
        protected abstract void SetColor(Color color);
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
        private void OnThemeColorChanged(ThemeData themeData, ColorData colorData) => TrySetColor(Theme.Instance.CurrentTheme);
    }
}