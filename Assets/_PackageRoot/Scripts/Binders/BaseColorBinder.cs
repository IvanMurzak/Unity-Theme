using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract partial class BaseColorBinder : MonoBehaviour
    {
        [SerializeField] protected ColorBinderData data;

        protected virtual IEnumerable<Object> ColorTargets { get; } = null;

        protected virtual void Awake()
        {
            if (data == null)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"ColorBinderData is null at <b>{GameObjectPath()}</b>, replacing it by first color", gameObject);

                data = new ColorBinderData() { colorGuid = Theme.Instance?.GetColorFirst().Guid };
                SetDirty();
            }
            if (!data.IsConnected)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color not found in database at <b>{GameObjectPath()}</b> Guid={data.colorGuid}", gameObject);

                var colorData = Theme.Instance?.GetColorFirst();
                if (colorData != null)
                {
                    data.colorGuid = colorData.Guid;
                    SetDirty();
                }
                else
                {
                    if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                        Debug.LogError($"First color not found in database.", gameObject);
                }
            }
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
                    Debug.LogError($"Current theme is null at gameObject {name}", gameObject);
                return;
            }
            
            var colorData = theme.GetColorByGuid(data.colorGuid);
            if (colorData == null)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"color not found by name '{data.ColorName}' at <b>{GameObjectPath()}</b>, guid='{data.colorGuid}'", gameObject);
            }
            else
            {
                var color = GetColor(colorData);
                if (Theme.Instance?.debugLevel <= DebugLevel.Log)
                    Debug.Log($"SetColor: '<b>{data.ColorName}</b>' {color.ToHexRGBA()} at <b>{GameObjectPath()}</b>", gameObject);
                SetColor(color);
            }
        }

        protected virtual Color GetColor(ColorData colorData)
        {
            var result = colorData.color;
            
            if (data.overrideAlpha) 
                result.a = data.alpha;

            return result;
        }
        protected abstract void SetColor(Color color);

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(data.colorGuid))
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"colorGuid is null at: <b>{GameObjectPath()}</b>. Taking the first one available.", gameObject);

                data.colorGuid = Theme.Instance?.GetColorFirst().Guid;
                SetDirty();
            }
            if (!data.IsConnected)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"colorGuid='{data.colorGuid}' doesn't match to any existed colors at: <b>{GameObjectPath()}</b>. Taking the first one available.", gameObject);

                data.colorGuid = Theme.Instance?.GetColorFirst().Guid;
                SetDirty();
            }

            TrySetColor(Theme.Instance.CurrentTheme);
        }
#endif
        private void SetDirty(Object obj)
        {
#if UNITY_EDITOR
            if (UnityEditor.PrefabUtility.IsPartOfAnyPrefab(obj))
                UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(obj);
            else
                UnityEditor.EditorUtility.SetDirty(obj);
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