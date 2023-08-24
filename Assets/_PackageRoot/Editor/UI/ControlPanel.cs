using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

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
            if (ThemeDatabaseInitializer.Config?.debugLevel <= DebugLevel.Log)
                Debug.Log(message);
            saveChangesMessage = message;
            base.SaveChanges();
        }

        public void CreateGUI()
        {
            var config = ThemeDatabaseInitializer.Config;
            
            var root = new ScrollView();
            rootVisualElement.Add(root);

            var panel = TemplateControlPanel.Instantiate();
            root.Add(panel);
            
            var dropdownCurrentTheme = panel.Query<DropdownField>("dropdownCurrentTheme").First();
            var toggleDebug = panel.Query<EnumField>("dropdownDebugLevel").First();

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
            
            toggleDebug.value = config.debugLevel;
            toggleDebug
                .RegisterValueChangedCallback(evt => 
                {
                    config.debugLevel = (DebugLevel)evt.newValue;
                    SaveChanges($"Debug status changed: {evt.newValue}");
                });
  
            // Colors
            // -----------------------------------------------------------------

            var inputFieldNewColorName = panel
                .Query<VisualElement>("contHeaderColors").First()
                .Query<TextField>("textFieldNewName").First();

            var btnCreateNewColor = panel
                .Query<VisualElement>("contHeaderColors").First()
                .Query<Button>("btnCreateNew").First();

            btnCreateNewColor.RegisterCallback<ClickEvent>(evt =>
            {
                config.AddTheme(inputFieldNewColorName.value);
                SaveChanges($"Color added: {inputFieldNewColorName.value}");
            });

            // Themes
            // -----------------------------------------------------------------

            var inputFieldNewThemeName = panel
                .Query<VisualElement>("contHeaderThemes").First()
                .Query<TextField>("textFieldNewName").First();

            var btnCreateNewTheme = panel
                .Query<VisualElement>("contHeaderThemes").First()
                .Query<Button>("btnCreateNew").First();

            var rootThemes = panel
                .Query<VisualElement>("rootThemes").First();

            btnCreateNewTheme.RegisterCallback<ClickEvent>(evt =>
            {
                config.AddColor(inputFieldNewColorName.value);
                SaveChanges($"Color added: {inputFieldNewColorName.value}");
            });

            var uiThemeColors = new Dictionary<string, Dictionary<string, UIThemeColor>>();

            foreach (var theme in config.Themes)
            {
                var themePanel = TemplateTheme.Instantiate();
                rootThemes.Add(themePanel);

                uiThemeColors[theme.themeName] = new Dictionary<string, UIThemeColor>();

                var foldoutTheme = themePanel.Query<Foldout>("foldoutTheme").First();
                var textFieldName = themePanel.Query<TextField>("txtName").First();
                var btnDeleteTheme = themePanel.Query<Button>("btnRemove").First();
                var contColors = themePanel.Query<VisualElement>("contColors").First();
                
                foldoutTheme.text = theme.themeName;

                textFieldName.value = theme.themeName;
                textFieldName.RegisterValueChangedCallback(evt =>
                {
                    theme.themeName = evt.newValue;
                    foldoutTheme.text = evt.newValue;
                    dropdownCurrentTheme.value = config.ThemeNames[config.CurrentThemeIndex];
                    SaveChanges($"Theme name changed: {evt.newValue}");
                });

                btnDeleteTheme.RegisterValueChangedCallback(evt =>
                {
                    config.RemoveTheme(theme);
                    rootThemes.Remove(themePanel);
                    uiThemeColors.Remove(theme.themeName);
                    SaveChanges($"Theme deleted: {theme.themeName}");
                });

                // Colors
                // ---------------------------
                foreach (var themeColor in theme.colors)
                {
                    var themeColorPanel = TemplateThemeColor.Instantiate();
                    contColors.Add(themeColorPanel);

                    var uiThemeColor = new UIThemeColor
                    {
                        root = themeColorPanel,
                        txtName = themeColorPanel.Query<TextField>("txtName").First(),
                        colorField = themeColorPanel.Query<ColorField>("color").First(),
                        btnDelete = themeColorPanel.Query<Button>("btnDelete").First()
                    };
                    uiThemeColors[theme.themeName][themeColor.Guid] = uiThemeColor;

                    uiThemeColor.txtName.value = config.GetColorName(themeColor.Guid);
                    uiThemeColor.colorField.value = themeColor.color;

                    uiThemeColor.txtName.RegisterValueChangedCallback(evt =>
                    {
                        var colorName = config.GetColorName(themeColor.Guid);
                        var colorRef = config.GetColorRef(themeColor.Guid);
                        
                        colorRef.name = evt.newValue;
                        config.UpdateColor(themeColor);
                        foreach (var uiTheme in uiThemeColors.Values)
                        {
                            if (uiTheme.ContainsKey(themeColor.Guid))
                                uiTheme[themeColor.Guid].txtName.value = evt.newValue;
                        }
                        SaveChanges($"Theme color[{colorRef.name}] changed: {evt.newValue}");
                    });

                    uiThemeColor.colorField.RegisterValueChangedCallback(evt =>
                    {
                        themeColor.color = evt.newValue;
                        config.SetColor(theme, themeColor);
                        SaveChanges($"Theme color[{config.GetColorName(themeColor.Guid)}] changed: {evt.newValue}");
                    });

                    uiThemeColor.btnDelete.clicked += () =>
                    {
                        var colorName = config.GetColorName(themeColor.Guid);
                        theme.colors.Remove(themeColor);
                        foldoutTheme.Remove(themeColorPanel);
                        foreach (var uiTheme in uiThemeColors.Values)
                        {
                            if (uiTheme.ContainsKey(themeColor.Guid))
                            {
                                uiTheme[themeColor.Guid].root.RemoveFromHierarchy();
                                uiTheme.Remove(themeColor.Guid);
                            }
                        }
                        SaveChanges($"Theme color[{colorName}] deleted");
                    };
                }
            }
        }

        struct UIThemeColor
        {
            public VisualElement root;
            public ColorField colorField;
            public TextField txtName;
            public Button btnDelete;
        }
    }
}