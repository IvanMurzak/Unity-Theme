using System.Linq;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract class BaseColorBinder : MonoBehaviour
    {
        [SerializeField] protected ColorBinderData data;

        protected virtual void Awake()
        {
            if (data == null)
            {
                data = new ColorBinderData()
                {
                    colorGuid = Theme.Instance?.GetColorFirst().Guid
                };
            }
            if (!data.IsConnected)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.Log($"Color not found in database. Guid={data.colorGuid}", gameObject);
                var colorData = Theme.Instance?.GetColorFirst();
                if (colorData != null)
                    data.colorGuid = colorData.Guid;
            }
            TrySetColor(Theme.Instance.CurrentTheme);
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
                    Debug.Log($"SetColor: '<b>{data.ColorName}</b>' #{color} at <b>{GameObjectPath()}</b>", gameObject);
                SetColor(color);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Attaching to first color
            if (string.IsNullOrEmpty(data.colorGuid) || Theme.Instance?.ColorGuids.Contains(data.colorGuid) == false)
                data.colorGuid = Theme.Instance?.GetColorFirst().Guid;

            TrySetColor(Theme.Instance.CurrentTheme);
        }
#endif
        protected virtual Color GetColor(ColorData colorData)
        {
            var result = colorData.color;
            
            if (data.overrideAlpha) 
                result.a = data.alpha;

            return result;
        }
        protected abstract void SetColor(Color color);

        private void OnThemeColorChanged(ThemeData themeData, ColorData colorData)
        {
            if (colorData.Guid == data.colorGuid)
                TrySetColor(Theme.Instance.CurrentTheme);
        }
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