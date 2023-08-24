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
        [SerializeField] VisualTreeAsset templateControlPanel;
        [SerializeField] VisualTreeAsset templateTheme;
        [SerializeField] VisualTreeAsset templateThemeColor;

        private Dictionary<string, UITheme> uiThemeColors = new Dictionary<string, UITheme>();
        private DropdownField dropdownCurrentTheme;

        [MenuItem("Window/Unity-Theme")]
        public static void ShowExample()
        {
            var wnd = GetWindow<ControlPanel>();
            wnd.titleContent = new GUIContent("Unity-Theme");
            wnd.Focus();
        }

        private void SaveChanges(string message)
        {
            if (ThemeDatabaseInitializer.Config?.debugLevel <= DebugLevel.Log)
                Debug.Log(message);
            saveChangesMessage = message;
            base.SaveChanges();
        }

        private void UpdateDropdownCurrentTheme(ThemeDatabase config)
        {
            dropdownCurrentTheme.choices = config.ThemeNames.ToList();
            dropdownCurrentTheme.value = config.CurrentThemeName;
        }

        public void CreateGUI()
        {
            var config = ThemeDatabaseInitializer.Config;            
            var panel = templateControlPanel.Instantiate();
            var root = new ScrollView();
            rootVisualElement.Add(root);
            root.Add(panel);
            
            // Settings
            // -----------------------------------------------------------------

            var enumDebugLevel = panel.Query<EnumField>("dropdownDebugLevel").First();
            dropdownCurrentTheme = panel.Query<DropdownField>("dropdownCurrentTheme").First();

            UpdateDropdownCurrentTheme(config);

            dropdownCurrentTheme.RegisterValueChangedCallback(evt => 
            {
                config.CurrentThemeName = evt.newValue;
                SaveChanges($"Theme Changed: {evt.newValue}");
            });
            
            enumDebugLevel.value = config.debugLevel;
            enumDebugLevel.RegisterValueChangedCallback(evt => 
            {
                config.debugLevel = (DebugLevel)evt.newValue;
                SaveChanges($"Debug status changed: {evt.newValue}");
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
                var themeName = inputFieldNewThemeName.value;
                inputFieldNewThemeName.value = "New Theme";
                var theme = config.AddTheme(themeName);
                
                UpdateDropdownCurrentTheme(config);

                UIAddTheme(config, rootThemes, theme);
                SaveChanges($"Theme added: {themeName}");
            });

            foreach (var theme in config.Themes)
                UIAddTheme(config, rootThemes, theme);
        }

        void UIAddTheme(ThemeDatabase config, VisualElement rootThemes, ThemeData theme)
        {
            var themePanel = templateTheme.Instantiate();
            rootThemes.Add(themePanel);

            var uiTheme = uiThemeColors[theme.Guid] = new UITheme()
            {
                root            = themePanel,
                btnDelete       = themePanel.Query<Button>("btnRemove").First(),
                foldoutTheme    = themePanel.Query<Foldout>("foldoutTheme").First(),
                textFieldName   = themePanel.Query<TextField>("textFieldName").First(),
                contColors      = themePanel.Query<VisualElement>("contColors").First(),
                theme           = theme,
                colors          = new Dictionary<string, UIThemeColor>()
            };

            themePanel.Query<Button>("btnAddColor").First().clicked += () =>
            {
                var colorName = "New";
                var themeColorRef = config.AddColor(colorName);
                var themeColor = new ColorData(themeColorRef);

                foreach (var uiTheme in uiThemeColors.Values)
                    UIAddThemeColor(config, uiTheme, new ColorData(themeColor));
                    
                SaveChanges($"Color added: {colorName}");
            };
            themePanel.Query<Button>("btnSortColors").First().clicked += () =>
            {
                // config.SortColors(theme);

                // foreach (var uiTheme in uiThemeColors.Values)
                //     UIAddThemeColor(config, uiTheme, new ColorData(themeColor));
                    
                SaveChanges($"Sorted colors");
            };            
            
            uiTheme.foldoutTheme.text = theme.themeName;
            uiTheme.textFieldName.value = theme.themeName;
            uiTheme.textFieldName.RegisterValueChangedCallback(evt =>
            {
                theme.themeName = evt.newValue;
                uiThemeColors[theme.Guid].foldoutTheme.text = evt.newValue;
                SaveChanges($"Theme name changed: {evt.newValue}");
            });
            uiTheme.btnDelete.clicked += () =>
            {
                config.RemoveTheme(theme);
                rootThemes.Remove(themePanel);
                uiThemeColors.Remove(theme.Guid);
                UpdateDropdownCurrentTheme(config);
                SaveChanges($"Theme deleted: {theme.themeName}");
            };

            foreach (var themeColor in theme.colors)
                UIAddThemeColor(config, uiTheme, themeColor);
        }
        void UIAddThemeColor(ThemeDatabase config, UITheme uiTheme, ColorData themeColor)
        {
            var themeColorPanel = templateThemeColor.Instantiate();
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