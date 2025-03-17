using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace Unity.Theme.Editor
{
    public class ThemeWindowEditor : EditorWindow
    {
        const string colorFillTemplateGuid = "07c8baad910b3e244bd677fa7d79370b";

        [SerializeField] VisualTreeAsset templateControlPanel;
        [SerializeField] VisualTreeAsset templateTheme;
        [SerializeField] VisualTreeAsset templateThemeColor;

        Dictionary<string, UITheme> uiThemeColors = new Dictionary<string, UITheme>();
        Dictionary<VisualElement, UIThemeColor> uiThemeColorsSet = new Dictionary<VisualElement, UIThemeColor>();
        DropdownField dropdownCurrentTheme;

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
                contColors      = themePanel.Query<ListView>("contColors").First(),
                contPreview     = themePanel.Query<VisualElement>("contPreview").First(),
                contContent     = themePanel.Query<VisualElement>("contContent").First(),
                theme           = theme,
                colors          = new Dictionary<string, UIThemeColor>()
            };

            themePanel.Query<Button>("btnAddColor").First().clicked += () =>
            {
                if (config.debugLevel.IsActive(DebugLevel.Trace))
                    Debug.Log($"[Theme] Adding new Color");

                var colorName = "New";
                var themeColorRef = config.AddColor(colorName);
                var themeColor = new ColorData(themeColorRef);

                foreach (var uiTheme in uiThemeColors.Values)
                {
                    theme.colors.Add(new ColorData(themeColor));
                    uiTheme.contColors.Rebuild();
                }

                SaveChanges($"Color added: {colorName}");
            };
            themePanel.Query<Button>("btnSortColors").First().clicked += () =>
            {
                config.SortColorsByName();
                SaveChanges($"Sorted colors");
            };

            uiTheme.contContent.style.display = new StyleEnum<DisplayStyle>(theme.expanded ? DisplayStyle.Flex : DisplayStyle.None);
            uiTheme.contColors.itemsSource = config.GetColors() as List<ColorDataRef>;
            uiTheme.contColors.selectionType = SelectionType.None;
            uiTheme.contColors.showFoldoutHeader = false;
            uiTheme.contColors.reorderable = true;
            uiTheme.contColors.reorderMode = ListViewReorderMode.Animated;

            uiTheme.contColors.itemIndexChanged += (oldIndex, newIndex) =>
            {
                if (config.debugLevel.IsActive(DebugLevel.Trace))
                    Debug.Log($"[Theme] Color moved: {oldIndex} -> {newIndex}");

                // Update order in other themes
                uiThemeColors
                    .Where(pair => pair.Key != theme.Guid)
                    .Select(pair => pair.Value)
                    .ToList()
                    .ForEach(otherTheme =>
                    {
                        otherTheme.contColors.Rebuild();
                        UIGenerateColorPreviews(config, otherTheme);
                    });

                UIGenerateColorPreviews(config, uiTheme);

                SaveChanges($"Color moved: {oldIndex} -> {newIndex}");
            };

            uiTheme.contColors.makeItem = templateThemeColor.Instantiate;
            uiTheme.contColors.bindItem = (ui, i) =>
            {
                var uiThemeColor = new UIThemeColor(config, theme, ui, i);
                uiThemeColorsSet[ui] = uiThemeColor;

                uiThemeColor.onNameChanged += (newName) =>
                {
                    var colorRef = config.GetColors()[i];
                    var themeColor = theme.GetColorByRef(colorRef);

                    colorRef.name = newName;
                    foreach (var uiTheme in uiThemeColors.Values)
                    {
                        if (uiTheme.colors.ContainsKey(themeColor.Guid))
                            uiTheme.colors[themeColor.Guid].txtName.value = newName;
                    }
                    SaveChanges($"Theme color[{colorRef.name}] changed: {newName}");
                };

                uiThemeColor.onColorChanged += (newColor) =>
                {
                    var colorRef = config.GetColors()[i];

                    config.UpdateColor(uiTheme.theme, colorRef.Guid, newColor);
                    UIGenerateColorPreviews(config, uiTheme);
                    SaveChanges($"Theme color[{config.GetColorName(colorRef.Guid)}] changed: {newColor}");
                };

                uiThemeColor.onDeleteRequest += () =>
                {
                    var colorRef = config.GetColors()[i];
                    var themeColor = theme.GetColorByRef(colorRef);

                    var colorName = config.GetColorName(colorRef.Guid);
                    uiTheme.theme.colors.Remove(themeColor);
                    // uiTheme.contColors.itemsSource.Remove(themeColorPanel);
                    // foreach (var uiTheme in uiThemeColors.Values)
                    // {
                    //     if (uiTheme.colors.ContainsKey(themeColor.Guid))
                    //     {
                    //         uiTheme.colors[themeColor.Guid].root.RemoveFromHierarchy();
                    //         uiTheme.colors.Remove(themeColor.Guid);
                    //     }
                    // }
                    config.RemoveColor(colorRef);
                    UIGenerateColorPreviews(config, uiTheme);
                    SaveChanges($"Theme color[{colorName}] deleted");
                };
            };
            uiTheme.contColors.unbindItem = (ui, i) =>
            {
                uiThemeColorsSet.GetValueOrDefault(ui)?.Dispose();
                uiThemeColorsSet.Remove(ui);
            };


            uiTheme.contColors.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
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

            UIGenerateColorPreviews(config, uiTheme);

            uiTheme.contColors.Rebuild();
        }
        // UIThemeColor UIAddThemeColor(Theme config, ThemeData theme, UITheme uiTheme, int orderIndex)
        // {
        //     //var themeColorPanel = templateThemeColor.Instantiate();
        //     //((List<VisualElement>)uiTheme.contColors.itemsSource).Add(themeColorPanel);

        //     uiTheme.contColors.Cr


        //     var themeColor = theme.colors[orderIndex];
        //     var uiThemeColor = new UIThemeColor(themeColorPanel);
        //     uiTheme.colors[themeColor.Guid] = uiThemeColor;

        //     UIColorBind(config, theme, uiTheme, uiThemeColor, orderIndex);
        //     return uiThemeColor;
        // }

        void UIGenerateColorPreviews(Theme config, UITheme uiTheme)
        {
            uiTheme.contPreview.Clear();
            var colorFillTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(colorFillTemplateGuid));
            foreach (var colorRef in config.GetColors())
            {
                var themeColor = uiTheme.theme.GetColorByRef(colorRef);
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
            public ListView contColors;
            public VisualElement contPreview;
            public VisualElement contContent;
            public ThemeData theme;
            public Dictionary<string, UIThemeColor> colors;
        }
        class UIThemeColor : IDisposable
        {
            public VisualElement root;
            public ColorField colorField;
            public TextField txtName;
            public Button btnDelete;

            public event Action<string> onNameChanged;
            public event Action<Color> onColorChanged;
            public event Action onDeleteRequest;

            readonly Theme config;
            readonly ThemeData theme;
            readonly int colorIndex;

            public UIThemeColor(Theme config, ThemeData theme, VisualElement root, int colorIndex)
            {
                this.root   = root;
                btnDelete   = root.Query<Button>("btnDelete").First();
                txtName     = root.Query<TextField>("txtName").First();
                colorField  = root.Query<ColorField>("color").First();

                this.config     = config;
                this.theme      = theme;
                this.colorIndex = colorIndex;

                UIColorBind();
            }

            void UIColorBind()
            {
                var colorRef = config.GetColors()[colorIndex];
                var themeColor = theme.GetColorByRef(colorRef);

                txtName.value = config.GetColorName(colorRef.Guid);
                colorField.value = themeColor.Color;

                txtName.RegisterValueChangedCallback(evt => onNameChanged?.Invoke(evt.newValue));
                colorField.RegisterValueChangedCallback(evt => onColorChanged?.Invoke(evt.newValue));
                btnDelete.clicked += () => onDeleteRequest?.Invoke();
            }

            public void Dispose()
            {
                root = null;
                colorField = null;
                txtName = null;
                btnDelete = null;

                onNameChanged = null;
                onColorChanged = null;
                onDeleteRequest = null;
            }
        }
    }
}