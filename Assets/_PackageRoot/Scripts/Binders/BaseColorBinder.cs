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
            get
            {
                var colorData = ThemeDatabaseInitializer.Config?.GetColorByGuid(colorGuid);
                if (colorData == null)
                    colorGuid = null;
                return colorData?.name;
            }
            set
            {
                var colorData = ThemeDatabaseInitializer.Config.GetColorByName(value);
                if (colorData != null)
                    colorGuid = colorData.guid;
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
                var colorData = ThemeDatabaseInitializer.Config?.GetColorFirst();
                if (colorData != null)
                    colorGuid = colorData.guid;
            }
            TrySetColor(ThemeDatabaseInitializer.Config.CurrentTheme);
        }
        protected virtual void OnEnable() => ThemeDatabaseInitializer.Config.onThemeChanged += TrySetColor;
        protected virtual void OnDisable() => ThemeDatabaseInitializer.Config.onThemeChanged -= TrySetColor;

        protected virtual void TrySetColor(ThemeData theme)
        {
            if (theme == null)
            {
                Debug.LogError($"Current theme is null at gameObject {name}", gameObject);
                return;
            }
            
            var colorData = theme.GetColorByGuid(colorGuid);
            if (colorData == null)
            {
                Debug.LogError($"color not found by name '{ColorName}' at <b>{GameObjectPath()}</b>, guid='{colorGuid}'", gameObject);
            }
            else
            {
                SetColor(GetColor(colorData));
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

        // UTILS ----------------------------------------------------------------------//
        private string GameObjectPath() => GameObjectPath(transform);                  //
        private static string GameObjectPath(Transform trans, string path = "")        //
        {                                                                              //
            if (trans == null) return path;                                            //
            if (string.IsNullOrEmpty(path))                                            //
                path = trans.name;                                                     //
            else                                                                       //
                path = $"{trans.name}/{path}";                                         //
            return GameObjectPath(trans.parent, path);                                 //
        }                                                                              //
        // ============================================================================//
    }
}