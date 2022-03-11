using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Unity.Theme.Binders
{
    [ExecuteAlways, ExecuteInEditMode]
    public abstract class BaseColorBinder : MonoBehaviour
    {
        [SerializeField, HideInInspector]                                   string          colorGuid;
        [ValueDropdown("GetColors"), ShowInInspector]                       string          ColorName
        {
            get => ThemeDatabaseInitializer.Config?.GetColorByGuid(colorGuid)?.name;
            set
            {
                var colorData = ThemeDatabaseInitializer.Config.GetColorByName(value);
                if (colorData != null)
                    colorGuid = colorData.guid;
            }
        }
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
                Debug.LogError("Current theme is null");
                return;
            }
            
            var colorData = theme.GetColorByGuid(colorGuid);
            if (colorData == null)
            {
                Debug.LogError($"color not found by key '{ColorName}'");
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