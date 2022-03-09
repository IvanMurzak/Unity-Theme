using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract class BaseColorBinder : MonoBehaviour
    {
        [ValueDropdown("GetColors"), Required, SerializeField]              string          colorName;
        [SerializeField]                                                    bool            overrideAlpha;
        [SerializeField, ShowIf("overrideAlpha"), PropertyRange(0f, 1f)]    float           alpha = 1f;

        private                                                             List<string>    GetColors() => ThemeDatabaseInitializer.Config.ColorNames;

        protected virtual void Awake()
        {
            TrySetColor(ThemeDatabaseInitializer.Config.CurrentTheme);
        }
        protected virtual void OnEnable() => ThemeDatabaseInitializer.Config.onThemeChanged += TrySetColor;
        protected virtual void OnDisable() => ThemeDatabaseInitializer.Config.onThemeChanged -= TrySetColor;

        protected virtual void TrySetColor(ThemeData theme)
        {
            if (theme == null)
            {
                Debug.LogError("Current theme is null");
                return;
            }
            var colorData = GetColorData(theme);
            if (colorData == null)
            {
                Debug.LogError($"color not found by key '{colorName}'");
            }
            else
            {
                SetColor(GetColor(colorData));
            }
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            TrySetColor(ThemeDatabaseInitializer.Config.CurrentTheme);
        }
#endif
        protected virtual ColorData GetColorData(ThemeData theme) => theme.colors.FirstOrDefault(x => x.name == colorName);
        protected virtual Color GetColor(ColorData colorData)
        {
            var result = colorData.color;
            
            if (overrideAlpha) 
                result.a = alpha;

            return result;
        }
        protected abstract void SetColor(Color color);

    }
}