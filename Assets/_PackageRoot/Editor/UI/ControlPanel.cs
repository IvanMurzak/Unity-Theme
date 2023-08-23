using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Unity.Theme.Editor
{
    public class ControlPanel : EditorWindow
    {
        public const string PATH = "Assets/_PackageRoot/Editor/UI";
        public static VisualTreeAsset TemplateControlPanel    => AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{PATH}/ControlPanel.uxml");
        public static VisualTreeAsset TemplateColor           => AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{PATH}/TemplateColor.uxml");
        public static VisualTreeAsset TemplateTheme           => AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{PATH}/TemplateTheme.uxml");
        public static VisualTreeAsset TemplateThemeColor      => AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{PATH}/TemplateThemeColor.uxml");

        [MenuItem("Window/Unity-Theme")]
        public static void ShowExample()
        {
            var wnd = GetWindow<ControlPanel>();
            wnd.titleContent = new GUIContent("ControlPanelEditor");
        }

        private void SaveChanges(string message)
        {
            Debug.Log(message);
            saveChangesMessage = message;
            base.SaveChanges();
        }

        public void CreateGUI()
        {
            var config = ThemeDatabaseInitializer.Config;
            var root = rootVisualElement;

            var panel = TemplateControlPanel.Instantiate();
            root.Add(panel);
            
            var dropdownCurrentTheme = panel.Query<DropdownField>("dropdownCurrentTheme").First();
            var toggleDebug = panel.Query<Toggle>("toggleDebug").First();

            // Header
            // -----------------------------------------------------------------

            dropdownCurrentTheme.choices = config.ThemeNames.ToList();
            dropdownCurrentTheme.value = config.ThemeNames[config.CurrentThemeIndex];
            dropdownCurrentTheme
                .RegisterValueChangedCallback(evt => 
                {
                    config.CurrentThemeName = evt.newValue;
                    SaveChanges($"Theme Changed: {evt.newValue}");
                });
            
            toggleDebug.value = config.debug;
            toggleDebug
                .RegisterValueChangedCallback(evt => 
                {
                    config.debug = evt.newValue;
                    SaveChanges($"Debug status changed: {evt.newValue}");
                });
  
            // Colors
            // -----------------------------------------------------------------

            // Themes
            // -----------------------------------------------------------------

            var themesRoot = panel
                .Query<ListView>("listView").First()
                .Query<VisualElement>("unity-content-container").First();

            foreach (var theme in config.Themes)
            {
                var themePanel = TemplateTheme.Instantiate();
                themesRoot.Add(themePanel);

                var foldoutTheme = themePanel.Query<Foldout>("foldoutTheme").First();
                var textFieldName = themePanel.Query<TextField>("txtName").First();
                
                foldoutTheme.text = theme.themeName;

                textFieldName.value = theme.themeName;
                textFieldName.RegisterValueChangedCallback(evt =>
                {
                    theme.themeName = evt.newValue;
                    foldoutTheme.text = evt.newValue;
                    dropdownCurrentTheme.value = config.ThemeNames[config.CurrentThemeIndex];
                    SaveChanges($"Theme name changed: {evt.newValue}");
                });

                // Colors
                // ---------------------------
                foreach (var themeColor in theme.colors)
                {
                    var themeColorPanel = TemplateThemeColor.Instantiate();
                    foldoutTheme.Add(themeColorPanel);

                    var txtColorName = themeColorPanel.Query<TextField>("txtName").First();
                    var colorField = themeColorPanel.Query<ColorField>("color").First();
                    var btnRename = themeColorPanel.Query<Button>("btnRename").First();
                    var btnDelete = themeColorPanel.Query<Button>("btnDelete").First();

                    txtColorName.value = themeColor.name;
                    colorField.value = themeColor.color;

                    colorField.RegisterValueChangedCallback(evt =>
                    {
                        themeColor.color = evt.newValue;
                        config.SetColor(theme, themeColor);
                        SaveChanges($"Theme color[{themeColor.name}] changed: {evt.newValue}");
                    });

                    btnDelete.clicked += () =>
                    {
                        theme.colors.Remove(themeColor);
                        foldoutTheme.Remove(themeColorPanel);
                        SaveChanges($"Theme color[{themeColor.name}] deleted");
                    };
                }
            }
        }
    }
}