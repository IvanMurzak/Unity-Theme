using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Unity.Theme.Editor.UI
{
    [CustomPropertyDrawer(typeof(ThemeData))]
    public class ThemeDataEditor : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property);
            // var root = rootVisualElement;
            // var config = ThemeDatabaseInitializer.Config;

            // var themePanel = ControlPanel.TemplateTheme.Instantiate();
            // themesRoot.Add(themePanel);

            // var foldoutTheme = themePanel.Query<Foldout>("foldoutTheme").First();
            // var textFieldName = themePanel.Query<TextField>("txtName").First();
            
            // foldoutTheme.text = theme.themeName;

            // textFieldName.value = theme.themeName;
            // textFieldName.RegisterValueChangedCallback(evt =>
            // {
            //     theme.themeName = evt.newValue;
            //     foldoutTheme.text = evt.newValue;
            //     dropdownCurrentTheme.value = config.ThemeNames[config.CurrentThemeIndex];
            // });
            // return base.GetPropertyColor(property);
        }
    }
}