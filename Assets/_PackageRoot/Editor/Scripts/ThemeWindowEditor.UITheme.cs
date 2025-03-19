using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using System;

namespace Unity.Theme.Editor
{
    public partial class ThemeWindowEditor : EditorWindow
    {
        class UITheme : IDisposable
        {
            public VisualElement root;
            public TextField textFieldName;
            public Button btnDelete;
            public Toggle toggleFoldout;
            public ListView listColors;
            public VisualElement contPreview;
            public VisualElement contContent;
            public ThemeData theme;
            public Dictionary<string, UIThemeColor> colors;

            public void RebuildColors(Theme config)
            {
                RebuildColorPreviews(config);
                listColors.Rebuild();
            }
            public void RebuildColorPreviews(Theme config)
            {
                contPreview.Clear();
                var colorFillTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(colorFillTemplateGuid));
                foreach (var colorRef in config.GetColors())
                {
                    var themeColor = theme.GetColorByRef(colorRef);
                    var colorFill = colorFillTemplate.Instantiate();
                    colorFill.Query<VisualElement>("colorFill").Last().style.unityBackgroundImageTintColor = new StyleColor(themeColor.Color);
                    contPreview.Add(colorFill);
                }
            }

            public void Dispose()
            {
                colors?.Clear();
                colors = null;
            }
        }
    }
}