using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract class BaseColorBinder : MonoBehaviour
    {
        [SerializeField, HideInInspector]                                   string          colorGuid;
        [HorizontalGroup("H"), ValueDropdown("GetColors"), ShowInInspector] string          ColorName
        {
            get => ThemeDatabaseInitializer.Config?.GetColorName(colorGuid);
            set
            {
                var colorData = ThemeDatabaseInitializer.Config.GetColorByName(value);
                if (colorData != null)
                    colorGuid = colorData.Guid;
            }
        }
        [GUIColor(1.0f, 0.5f, 0.5f)]
        [HorizontalGroup("H", 60), Button("RESET")]                         void            ResetColor() { colorGuid = null; }
        [SerializeField]                                                    bool            overrideAlpha;
        [SerializeField, ShowIf("overrideAlpha"), PropertyRange(0f, 1f)]    float           alpha = 1f;

        private                                                             List<string>    GetColors() => ThemeDatabaseInitializer.Config.ColorNames;

        protected virtual void Awake()
        {
            if (string.IsNullOrEmpty(ColorName))
            {
                if (ThemeDatabaseInitializer.Config?.debugLevel <= DebugLevel.Error)
                    Debug.Log($"Color not found in database. Guid={colorGuid}", gameObject);
                var colorData = ThemeDatabaseInitializer.Config?.GetColorFirst();
                if (colorData != null)
                    colorGuid = colorData.Guid;
            }
            TrySetColor(ThemeDatabaseInitializer.Config.CurrentTheme);
        }
        protected virtual void OnEnable()
        {
            TrySetColor(ThemeDatabaseInitializer.Config.CurrentTheme);
            ThemeDatabaseInitializer.Config.onThemeChanged += TrySetColor;
            ThemeDatabaseInitializer.Config.onThemeColorChanged += OnThemeColorChanged;
        }
        protected virtual void OnDisable()
        {
            ThemeDatabaseInitializer.Config.onThemeChanged -= TrySetColor;
            ThemeDatabaseInitializer.Config.onThemeColorChanged -= OnThemeColorChanged;
        }
        protected virtual void TrySetColor(ThemeData theme)
        {
            if (theme == null)
            {
                if (ThemeDatabaseInitializer.Config?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Current theme is null at gameObject {name}", gameObject);
                return;
            }
            
            var colorData = theme.GetColorByGuid(colorGuid);
            if (colorData == null)
            {
                if (ThemeDatabaseInitializer.Config?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"color not found by name '{ColorName}' at <b>{GameObjectPath()}</b>, guid='{colorGuid}'", gameObject);
            }
            else
            {
                var color = GetColor(colorData);
                if (ThemeDatabaseInitializer.Config?.debugLevel <= DebugLevel.Log)
                    Debug.Log($"SetColor: '<b>{ColorName}</b>' #{color} at <b>{GameObjectPath()}</b>", gameObject);
                SetColor(color);
            }
        }

#if UNITY_EDITOR
        private void OnValidate() => TrySetColor(ThemeDatabaseInitializer.Config.CurrentTheme);
#endif
        protected virtual Color GetColor(ColorData colorData)
        {
            var result = colorData.color;
            
            if (overrideAlpha) 
                result.a = alpha;

            return result;
        }
        protected abstract void SetColor(Color color);

        private void OnThemeColorChanged(ThemeData themeData, ColorData colorData)
        {
            if (colorData.Guid == colorGuid)
                TrySetColor(ThemeDatabaseInitializer.Config.CurrentTheme);
        }
        // UTILS ---------------------------------------------------------------------------//
        private string GameObjectPath() => GameObjectPath(transform);                       //
        private static string GameObjectPath(Transform trans, string path = "")             //
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