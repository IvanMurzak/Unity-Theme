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

        private Dictionary<string, UITheme> uiThemeColors = new Dictionary<string, UITheme>();

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
            rootVisualElement.Query<ScrollView>().First()?.RemoveFromHierarchy();
            uiThemeColors.Clear();

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
            dropdownCurrentTheme.RegisterValueChangedCallback(evt => 
            {
                config.CurrentThemeName = evt.newValue;
                SaveChanges($"Theme Changed: {evt.newValue}");
            });
            
            toggleDebug.value = config.debugLevel;
            toggleDebug.RegisterValueChangedCallback(evt => 
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

            btnCreateNewColor.clicked += () =>
            {
                var themeColorRef = config.AddColor(inputFieldNewColorName.value);
                var themeColor = new ColorData(themeColorRef);

                foreach (var uiTheme in uiThemeColors.Values)
                    UIAddThemeColor(config, uiTheme, themeColor);
                    
                SaveChanges($"Color added: {inputFieldNewColorName.value}");
            };

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
                var theme = config.AddTheme(inputFieldNewThemeName.value);
                UIAddTheme(config, rootThemes, theme);
                SaveChanges($"Theme added: {inputFieldNewThemeName.value}");
            });

            foreach (var theme in config.Themes)
                UIAddTheme(config, rootThemes, theme);
        }

        void UIAddTheme(ThemeDatabase config, VisualElement rootThemes, ThemeData theme)
        {
            var themePanel = TemplateTheme.Instantiate();
            rootThemes.Add(themePanel);

            var uiTheme = uiThemeColors[theme.themeName] = new UITheme()
            {
                root            = themePanel,
                btnDelete       = themePanel.Query<Button>("btnRemove").First(),
                foldoutTheme    = themePanel.Query<Foldout>("foldoutTheme").First(),
                textFieldName   = themePanel.Query<TextField>("textFieldName").First(),
                contColors      = themePanel.Query<VisualElement>("contColors").First(),
                theme           = theme,
                colors          = new Dictionary<string, UIThemeColor>()
            };
            
            uiTheme.foldoutTheme.text = theme.themeName;
            uiTheme.textFieldName.value = theme.themeName;
            uiTheme.textFieldName.RegisterValueChangedCallback(evt =>
            {
                theme.themeName = evt.newValue;
                uiThemeColors[theme.themeName].foldoutTheme.text = evt.newValue;
                SaveChanges($"Theme name changed: {evt.newValue}");
            });
            uiTheme.btnDelete.clicked += () =>
            {
                config.RemoveTheme(theme);
                rootThemes.Remove(themePanel);
                uiThemeColors.Remove(theme.themeName);
                SaveChanges($"Theme deleted: {theme.themeName}");
            };

            foreach (var themeColor in theme.colors)
                UIAddThemeColor(config, uiTheme, themeColor);
        }
        void UIAddThemeColor(ThemeDatabase config, UITheme uiTheme, ColorData themeColor)
        {
            var themeColorPanel = TemplateThemeColor.Instantiate();
            uiTheme.contColors.Add(themeColorPanel);

            var uiThemeColor = new UIThemeColor
            {
                root        = themeColorPanel,
                btnDelete   = themeColorPanel.Query<Button>("btnDelete").First(),
                txtName     = themeColorPanel.Query<TextField>("txtName").First(),
                colorField  = themeColorPanel.Query<ColorField>("color").First()
            };
            uiTheme.colors[themeColor.Guid] = uiThemeColor;

            uiThemeColor.txtName.value = config.GetColorName(themeColor.Guid);
            uiThemeColor.colorField.value = themeColor.color;

            uiThemeColor.txtName.RegisterValueChangedCallback(evt =>
            {
                var colorName = config.GetColorName(themeColor.Guid);
                var colorRef = config.GetColorRef(themeColor.Guid);

                colorRef.name = evt.newValue;
                foreach (var uiTheme in uiThemeColors.Values)
                {
                    if (uiTheme.colors.ContainsKey(themeColor.Guid))
                        uiTheme.colors[themeColor.Guid].txtName.value = evt.newValue;
                }
                SaveChanges($"Theme color[{colorRef.name}] changed: {evt.newValue}");
            });

            uiThemeColor.colorField.RegisterValueChangedCallback(evt =>
            {
                themeColor.color = evt.newValue;
                config.UpdateColor(uiTheme.theme, themeColor);
                SaveChanges($"Theme color[{config.GetColorName(themeColor.Guid)}] changed: {evt.newValue}");
            });

            uiThemeColor.btnDelete.clicked += () =>
            {
                var colorName = config.GetColorName(themeColor.Guid);
                uiTheme.theme.colors.Remove(themeColor);
                uiTheme.contColors.Remove(themeColorPanel);
                foreach (var uiTheme in uiThemeColors.Values)
                {
                    if (uiTheme.colors.ContainsKey(themeColor.Guid))
                    {
                        uiTheme.colors[themeColor.Guid].root.RemoveFromHierarchy();
                        uiTheme.colors.Remove(themeColor.Guid);
                    }
                }
                config.RemoveColor(themeColor);
                SaveChanges($"Theme color[{colorName}] deleted");
            };
        }

        struct UITheme
        {
            public VisualElement root;
            public Foldout foldoutTheme;
            public TextField textFieldName;
            public Button btnDelete;
            public VisualElement contColors;
            public ThemeData theme;
            public Dictionary<string, UIThemeColor> colors;
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