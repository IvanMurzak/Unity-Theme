using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.Theme.Editor
{
    public partial class ThemeWindowEditor : EditorWindow
    {
        const string colorFillTemplateGuid = "07c8baad910b3e244bd677fa7d79370b";

        [SerializeField] VisualTreeAsset templateControlPanel;
        [SerializeField] VisualTreeAsset templateTheme;
        [SerializeField] VisualTreeAsset templateThemeColor;

        Dictionary<string, UITheme> uiThemes = new Dictionary<string, UITheme>();
        Dictionary<VisualElement, UIThemeColor> uiThemeColors = new Dictionary<VisualElement, UIThemeColor>();
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
        void OnValidate() => Theme.Instance.OnValidate();
        void OnUndoRedoPerformed()
        {
            if (Theme.IsLogActive(DebugLevel.Trace))
                Debug.Log("Undo or redo performed");
            Invalidate();
        }

        private void SaveChanges(string message)
        {
            if (Theme.IsLogActive(DebugLevel.Log))
                Debug.Log(message);
            saveChangesMessage = message;

            Undo.RecordObject(Theme.Instance.AssetFile, message); // Undo record started
            base.SaveChanges();
            Theme.Instance.Save();
            Theme.Instance.InvalidateAssetFile();
            EditorUtility.SetDirty(Theme.Instance.AssetFile); // Undo record completed
        }

        private void UpdateDropdownCurrentTheme(Theme config)
        {
            dropdownCurrentTheme.choices = config.ThemeNames.ToList();
            dropdownCurrentTheme.value = config.CurrentThemeName;
        }
        private void OnThemeChanged(ThemeData themeData) => Repaint();

        private void OnEnable()
        {
            Theme.Instance.onThemeChanged += OnThemeChanged;
            // Undo.undoRedoPerformed += OnUndoRedoPerformed;
        }
        private void OnDisable()
        {
            Theme.Instance.onThemeChanged -= OnThemeChanged;
            // Undo.undoRedoPerformed -= OnUndoRedoPerformed;
        }
        public void CreateGUI()
        {
            rootVisualElement.Clear();

            var config = Theme.Instance;
            var panel = templateControlPanel.Instantiate();
            var root = new ScrollView();
            rootVisualElement.Add(root);
            root.Add(panel);

            uiThemes.Clear();
            uiThemeColors.Clear();

            // Settings
            // -----------------------------------------------------------------

            var enumDebugLevel = panel.Query<EnumField>("dropdownDebugLevel").First();
            dropdownCurrentTheme = panel.Query<DropdownField>("dropdownCurrentTheme").First();

            UpdateDropdownCurrentTheme(config);

            dropdownCurrentTheme.RegisterValueChangedCallback(evt =>
            {
                config.CurrentThemeName = evt.newValue;
                SaveChanges($"[Theme] Theme Changed: {evt.newValue}");
            });

            enumDebugLevel.value = config.debugLevel;
            enumDebugLevel.RegisterValueChangedCallback(evt =>
            {
                config.debugLevel = (DebugLevel)evt.newValue;
                SaveChanges($"[Theme] Debug status changed: {evt.newValue}");
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
                SaveChanges($"[Theme] Theme added: {themeName}");
            });

            foreach (var theme in config.Themes)
                UIAddTheme(config, rootThemes, theme);
        }

        void RebuildThemeColors(Theme config)
        {
            foreach (var uiTheme in uiThemes.Values)
                uiTheme.RebuildColors(config);
        }

        void UIAddTheme(Theme config, VisualElement rootThemes, ThemeData theme)
        {
            var themePanel = templateTheme.Instantiate();
            rootThemes.Add(themePanel);

            var uiTheme = uiThemes[theme.Guid] = new UITheme()
            {
                root            = themePanel,
                btnDelete       = themePanel.Query<Button>("btnRemove").First(),
                toggleFoldout   = themePanel.Query<Toggle>("toggleFoldout").First(),
                textFieldName   = themePanel.Query<TextField>("textFieldName").First(),
                listColors      = themePanel.Query<ListView>("listColors").First(),
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

                foreach (var uiTheme in uiThemes.Values)
                {
                    theme.colors.Add(new ColorData(themeColor));
                    uiTheme.listColors.Rebuild();
                }

                SaveChanges($"[Theme] Color added: {colorName}");
            };
            themePanel.Query<Button>("btnSortColors").First().clicked += () =>
            {
                config.SortColorsByName();
                RebuildThemeColors(config);
                SaveChanges($"[Theme] Sorted colors");
            };

            uiTheme.contContent.style.display = new StyleEnum<DisplayStyle>(theme.expanded ? DisplayStyle.Flex : DisplayStyle.None);
            uiTheme.listColors.itemsSource = config.GetColors() as List<ColorDataRef>;
            uiTheme.listColors.selectionType = SelectionType.None;
            uiTheme.listColors.showFoldoutHeader = false;
            uiTheme.listColors.reorderable = true;
            uiTheme.listColors.reorderMode = ListViewReorderMode.Animated;

            uiTheme.listColors.itemIndexChanged += (oldIndex, newIndex) =>
            {
                if (config.debugLevel.IsActive(DebugLevel.Trace))
                    Debug.Log($"[Theme] Color moved: {oldIndex} -> {newIndex} (started)");

                // Update order in other themes
                uiThemes
                    .Where(pair => pair.Key != theme.Guid)
                    .Select(pair => pair.Value)
                    .ToList()
                    .ForEach(otherTheme => otherTheme.RebuildColors(config));

                uiTheme.RebuildColorPreviews(config);

                SaveChanges($"[Theme] Color moved: {oldIndex} -> {newIndex}");
            };

            uiTheme.listColors.makeItem = templateThemeColor.Instantiate;
            uiTheme.listColors.bindItem = (ui, i) =>
            {
                var uiThemeColor = new UIThemeColor(config, theme, ui, i);

                uiTheme.colors[uiThemeColor.ColorGuid] = uiThemeColor;
                uiThemeColors[ui] = uiThemeColor;

                uiThemeColor.onNameChanged += (newName) =>
                {
                    var colorRef = config.GetColorByIndex(i);
                    if (colorRef == null)
                    {
                        if (config.debugLevel.IsActive(DebugLevel.Error))
                            Debug.LogError($"[Theme] ColorRef is null");
                        return;
                    }
                    var themeColor = theme.GetColorByRef(colorRef);

                    colorRef.name = newName;
                    foreach (var uiTheme in uiThemes.Values)
                    {
                        if (uiTheme.colors.TryGetValue(themeColor.Guid, out var uiThemeColor))
                            uiThemeColor.txtName.value = newName;
                    }
                    SaveChanges($"[Theme] Color[{colorRef.name}] changed: {newName}");
                };

                uiThemeColor.onColorChanged += (newColor) =>
                {
                    var colorRef = config.GetColorByIndex(i);
                    if (colorRef == null)
                    {
                        if (config.debugLevel.IsActive(DebugLevel.Error))
                            Debug.LogError($"[Theme] ColorRef is null");
                        return;
                    }

                    config.UpdateColor(uiTheme.theme, colorRef.Guid, newColor);
                    uiTheme.RebuildColorPreviews(config);
                    SaveChanges($"[Theme] Color[{config.GetColorName(colorRef.Guid)}] changed: {newColor}");
                };

                uiThemeColor.onDeleteRequest += () =>
                {
                    var colorRef = config.GetColorByIndex(i);
                    if (colorRef == null)
                    {
                        if (config.debugLevel.IsActive(DebugLevel.Error))
                            Debug.LogError($"[Theme] ColorRef is null");
                        return;
                    }
                    config.RemoveColor(colorRef);

                    RebuildThemeColors(config);
                    SaveChanges($"[Theme] Color[{colorRef.name}] deleted");
                };
            };
            uiTheme.listColors.unbindItem = (ui, i) =>
            {
                var uiThemeColor = uiThemeColors.GetValueOrDefault(ui);
                if (uiThemeColor == null)
                    return;

                uiTheme.colors.Remove(uiThemeColor.ColorGuid);
                uiThemeColor.Dispose();
                uiThemeColors.Remove(ui);
            };

            uiTheme.listColors.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            uiTheme.toggleFoldout.value = theme.expanded;
            uiTheme.toggleFoldout.RegisterValueChangedCallback(evt =>
            {
                uiTheme.contContent.style.display = new StyleEnum<DisplayStyle>(evt.newValue ? DisplayStyle.Flex : DisplayStyle.None);
                theme.expanded = evt.newValue;
                // SaveChanges($"[Theme] Theme foldout changed: {evt.newValue}");
            });

            uiTheme.textFieldName.value = theme.themeName;
            uiTheme.textFieldName.RegisterValueChangedCallback(evt =>
            {
                theme.themeName = evt.newValue;
                SaveChanges($"[Theme] Theme name changed: {evt.newValue}");
            });
            uiTheme.btnDelete.clicked += () =>
            {
                config.RemoveTheme(theme);
                rootThemes.Remove(themePanel);
                uiThemes.Remove(theme.Guid);
                UpdateDropdownCurrentTheme(config);
                SaveChanges($"[Theme] Theme deleted: {theme.themeName}");
            };

            uiTheme.RebuildColors(config);
        }
    }
}