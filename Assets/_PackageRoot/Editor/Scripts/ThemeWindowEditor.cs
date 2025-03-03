using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Unity.Theme.Editor
{
    public class ThemeWindowEditor : EditorWindow
    {
        const string colorFillTemplateGuid = "07c8baad910b3e244bd677fa7d79370b";

        [SerializeField] VisualTreeAsset templateControlPanel;
        [SerializeField] VisualTreeAsset templateTheme;
        [SerializeField] VisualTreeAsset templateThemeColor;

        private Dictionary<string, UITheme> uiThemeColors = new Dictionary<string, UITheme>();
        private DropdownField dropdownCurrentTheme;

        [MenuItem("Window/Unity-Theme")]
        public static ThemeWindowEditor ShowWindow()
        {
            var window = GetWindow<ThemeWindowEditor>();
            window.titleContent = new GUIContent("Unity-Theme");
            window.Focus();
            return window;
        }
        public static void ShowWindowVoid() => ShowWindow();

        public void Invalidate() => CreateGUI();
        void OnValidate()
        {
            Theme.Instance.OnValidate();
        }

        private void SaveChanges(string message)
        {
            if (Theme.IsLogActive(DebugLevel.Log))
                Debug.Log(message);
            saveChangesMessage = message;
            base.SaveChanges();
            Theme.Instance.Save();
            Undo.RecordObject(Theme.Instance.AssetFile, message);
        }

        private void UpdateDropdownCurrentTheme(Theme config)
        {
            dropdownCurrentTheme.choices = config.ThemeNames.ToList();
            dropdownCurrentTheme.value = config.CurrentThemeName;
        }
        private void OnThemeChanged(ThemeData themeData) => Repaint();

        private void OnEnable() => Theme.Instance.onThemeChanged += OnThemeChanged;
        private void OnDisable() => Theme.Instance.onThemeChanged -= OnThemeChanged;
        public void CreateGUI()
        {
            rootVisualElement.Clear();

            var config = Theme.Instance;
            var panel = templateControlPanel.Instantiate();
            var root = new ScrollView();
            rootVisualElement.Add(root);
            root.Add(panel);

            uiThemeColors.Clear();

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

        void UIAddTheme(Theme config, VisualElement rootThemes, ThemeData theme)
        {
            var themePanel = templateTheme.Instantiate();
            rootThemes.Add(themePanel);

            var uiTheme = uiThemeColors[theme.Guid] = new UITheme()
            {
                root            = themePanel,
                btnDelete       = themePanel.Query<Button>("btnRemove").First(),
                toggleFoldout   = themePanel.Query<Toggle>("toggleFoldout").First(),
                textFieldName   = themePanel.Query<TextField>("textFieldName").First(),
                contColors      = themePanel.Query<VisualElement>("contColors").First(),
                contPreview     = themePanel.Query<VisualElement>("contPreview").First(),
                contContent     = themePanel.Query<VisualElement>("contContent").First(),
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
                config.SortColorsByName();
                SaveChanges($"Sorted colors");
            };

            uiTheme.contContent.style.display = new StyleEnum<DisplayStyle>(theme.expanded ? DisplayStyle.Flex : DisplayStyle.None);
            uiTheme.toggleFoldout.value = theme.expanded;
            uiTheme.toggleFoldout.RegisterValueChangedCallback(evt =>
            {
                uiTheme.contContent.style.display = new StyleEnum<DisplayStyle>(evt.newValue ? DisplayStyle.Flex : DisplayStyle.None);
                theme.expanded = evt.newValue;
                // SaveChanges($"Theme foldout changed: {evt.newValue}");
            });

            uiTheme.textFieldName.value = theme.themeName;
            uiTheme.textFieldName.RegisterValueChangedCallback(evt =>
            {
                theme.themeName = evt.newValue;
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

            UIGenerateColorPreviews(uiTheme);

            foreach (var themeColor in theme.colors)
                UIAddThemeColor(config, uiTheme, themeColor);
        }
        void UIAddThemeColor(Theme config, UITheme uiTheme, ColorData themeColor)
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
            uiThemeColor.colorField.value = themeColor.Color;

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
                config.UpdateColor(uiTheme.theme, themeColor.Guid, evt.newValue);
                UIGenerateColorPreviews(uiTheme);
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
                UIGenerateColorPreviews(uiTheme);
                SaveChanges($"Theme color[{colorName}] deleted");
            };
        }
        void UIGenerateColorPreviews(UITheme uiTheme)
        {
            uiTheme.contPreview.Clear();
            var colorFillTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(colorFillTemplateGuid));
            foreach (var themeColor in uiTheme.theme.colors)
            {
                var colorFill = colorFillTemplate.Instantiate();
                colorFill.Query<VisualElement>("colorFill").Last().style.unityBackgroundImageTintColor = new StyleColor(themeColor.Color);
                uiTheme.contPreview.Add(colorFill);
            }
        }

        struct UITheme
        {
            public VisualElement root;
            public TextField textFieldName;
            public Button btnDelete;
            public Toggle toggleFoldout;
            public VisualElement contColors;
            public VisualElement contPreview;
            public VisualElement contContent;
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