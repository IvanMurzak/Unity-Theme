using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Unity.Theme.EditorUI
{
    [CustomEditor(typeof(ThemeDatabase))]
    public partial class ThemeDatabaseEditor : Editor
    {
        const string AssetsPath = "Assets/_PackageRoot/Editor/Scripts/UIElements";

        ThemeDatabase   themeDatabase;
        VisualElement   rootElement;
        VisualElement   columnColorNames;
        VisualElement   columnRemoveColors;
        VisualElement   themeColumns;
        TextField       textNewColorName;
        Button          btnAddColor;
        // Button          btnAddTheme;
        VisualTreeAsset templateColumnTheme;
        VisualTreeAsset templateColumnThemeAdd;
        VisualTreeAsset templateColorName;
        VisualTreeAsset templateColorRemove;
        VisualTreeAsset templateColor;

        public override VisualElement CreateInspectorGUI()
        {
            themeDatabase = (ThemeDatabase)target;
            rootElement = new VisualElement();

            // Load in UXML template and USS styles, then apply them to the root element.
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{AssetsPath}/ThemeDatabaseEditor.uxml");
            visualTree.CloneTree(rootElement);

            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{AssetsPath}/ThemeDatabaseEditor.uss");
            rootElement.styleSheets.Add(stylesheet);

            columnColorNames        = rootElement.Query<VisualElement>  ("columnColorNames").First();
            columnRemoveColors      = rootElement.Query<VisualElement>  ("columnRemoveColors").First();
            themeColumns            = rootElement.Query<VisualElement>  ("themeColumns")    .First();
            textNewColorName        = rootElement.Query<TextField>      ("textNewColorName").First();
            btnAddColor             = rootElement.Query<Button>         ("btnAddColor")     .First();

            templateColumnTheme     = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{AssetsPath}/ColumnTheme.uxml");
            templateColumnThemeAdd  = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{AssetsPath}/ColumnThemeAdd.uxml");
            templateColorName       = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{AssetsPath}/ColorName.uxml");
            templateColorRemove     = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{AssetsPath}/ColorRemove.uxml");
            templateColor           = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{AssetsPath}/Color.uxml");

            GUIColorsColumn();
            GUIColorAdd();
            GUIThemeColumns();
            GUIThemeAdd();

            return rootElement;
        }

        void GUIColorsColumn()
        {
            foreach (var colorNameData in themeDatabase.colorNames)
            {
                var colorName = new VisualElement();
                templateColorName.CloneTree(colorName);
                var textColorName = colorName.Q<TextField>("colorName");
                textColorName.SetValueWithoutNotify(colorNameData.name);
                textColorName.RegisterValueChangedCallback(x =>
                {
                    themeDatabase.ChangeColorName(colorNameData.name, x.newValue);
                    RefreshGraphics();
                });
                columnColorNames.Add(colorName);

                var colorRemove = templateColorRemove.CloneTree();
                colorRemove.Q<Button>("btnRemoveColor").clicked += () =>
                {
                    themeDatabase.RemoveColor(textColorName.text);
                    OnInspectorGUI();
                    RefreshGraphics();
                };
                columnRemoveColors.Add(colorRemove);
            }
        }
        void GUIColorAdd()
        {
            textNewColorName.SetValueWithoutNotify("New Color");
            btnAddColor.clicked += () =>
            {
                if (themeDatabase.AddColor(textNewColorName.text))
                    textNewColorName.SetValueWithoutNotify("New Color");
                RefreshGraphics();
            };
        }
        void GUIThemeColumns()
        {
            foreach (var theme in themeDatabase.themes)
            {
                var tempTheme           = theme;
                var themeColumn         = templateColumnTheme.CloneTree();
                var columnThemeColors   = themeColumn.Q<VisualElement>  ("columnThemeColors");
                var btnRemoveTheme      = themeColumn.Q<Button>         ("btnRemoveTheme");
                var textThemeName       = themeColumn.Q<TextField>      ("themeName");

                textThemeName.SetValueWithoutNotify(theme.themeName);
                textThemeName.RegisterValueChangedCallback(x =>
                {
                    themeDatabase.ChangeThemeName(theme.themeName, x.newValue);
                    RefreshGraphics();
                });

                btnRemoveTheme.clicked += () =>
                {
                    themeDatabase.RemoveTheme(tempTheme);
                    RefreshGraphics();
                };

                themeColumns.Add(themeColumn);
            }
        }
        void GUIThemeAdd()
        {
            var themeColumnAdd      = templateColumnThemeAdd.CloneTree();
            var btnThemeAdd         = themeColumnAdd.Q<Button>("btnAddTheme");
            var textThemeNameAdd    = themeColumnAdd.Q<TextField>("themeName");

            btnThemeAdd.clicked += () =>
            {
                themeDatabase.AddTheme(textThemeNameAdd.text);
                RefreshGraphics();
            };
            
            themeColumns.Add(themeColumnAdd);
        }
        private void RefreshGraphics()
        {
            Selection.activeObject = null;
            EditorApplication.delayCall += OnDelayedCall;
        }
        private void OnDelayedCall()
        {
            EditorApplication.update -= OnDelayedCall;
            Selection.activeObject = target;
        }
    }
}