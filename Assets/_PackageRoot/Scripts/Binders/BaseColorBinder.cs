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
                    Debug.LogError($"Color not found in database at <b>{GameObjectPath()}</b> Guid={data.colorGuid}", gameObject);
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
            TrySetColor(Theme.Instance.CurrentTheme);
            Theme.Instance.onThemeChanged += TrySetColor;
            Theme.Instance.onThemeColorChanged += OnThemeColorChanged;
        }
        protected virtual void OnDisable()
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
                var targetColor = GetTargetColor(colorData);
                var currentColor = GetColor();
                if (targetColor == currentColor)
                    return; // skip if color is the same

                if (Theme.Instance?.debugLevel <= DebugLevel.Log)
                    Debug.Log($"SetColor: '<b>{data.ColorName}</b>' {targetColor.ToHexRGBA()} at <b>{GameObjectPath()}</b>", gameObject);
                SetColor(targetColor);
                SetDirty();
            }
        }

        protected virtual Color GetTargetColor(ColorData colorData)
        {
            var result = colorData.color;
            
            if (data.overrideAlpha) 
                result.a = data.alpha;

            return result;
        }
        protected abstract void SetColor(Color color);
        protected abstract Color? GetColor();

        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(data.colorGuid))
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color GUID is <b><color=red>null</color></b> at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
            if (!data.IsConnected)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color with GUID='{data.colorGuid}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
#if UNITY_EDITOR
            TrySetColor(Theme.Instance.CurrentTheme);
#endif
        }
        private void SetDirty(Object obj)
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(obj);
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(obj);
#endif
        }
        private void OnThemeColorChanged(ThemeData themeData, ColorData colorData) => TrySetColor(Theme.Instance.CurrentTheme);

        // UTILS ---------------------------------------------------------------------------//
        protected string GameObjectPath() => GameObjectPath(transform);                     //
        protected static string GameObjectPath(Transform trans, string path = "")           //
        {                                                                                   //
            if (string.IsNullOrEmpty(path))                                                 //
                path = trans.name;                                                          //
            else                                                                            //
                path = $"{trans.name}/{path}";                                              //
                                                                                            //
            if (trans.parent == null)                                                       //
            {                                                                               //
                var isPrefab = string.IsNullOrEmpty(trans.gameObject.scene.name);           //
                if (isPrefab)                                                               //
                    path = $"<color=cyan>Prefabs</color>/{path}";                           //
                else                                                                        //
                    path = $"<color=cyan>{trans.gameObject.scene.name}</color>/{path}";     //
                return path;                                                                //
            }                                                                               //
            else                                                                            //
            {                                                                               //
                return GameObjectPath(trans.parent, path);                                  //
            }                                                                               //
        }                                                                                   //
        // =================================================================================//
    }
}