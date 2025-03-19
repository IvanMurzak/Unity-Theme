using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace Unity.Theme.Editor
{
    public partial class ThemeWindowEditor : EditorWindow
    {
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

            public string ColorGuid { get; private set; }

            public UIThemeColor(Theme config, ThemeData theme, VisualElement root, int colorIndex)
            {
                this.root   = root;
                btnDelete   = root.Query<Button>("btnDelete").First();
                txtName     = root.Query<TextField>("txtName").First();
                colorField  = root.Query<ColorField>("color").First();

                this.config     = config;
                this.theme      = theme;
                this.colorIndex = colorIndex;

                ColorGuid = config.GetColorByIndex(colorIndex).Guid;

                UIColorBind();
            }

            void UIColorBind()
            {
                var colorRef = config.GetColorByIndex(colorIndex);
                if (colorRef == null)
                {
                    if (config.debugLevel.IsActive(DebugLevel.Error))
                        Debug.LogError($"[Theme] ColorRef is null");
                    return;
                }
                ColorGuid = colorRef.Guid;
                txtName.value = config.GetColorName(colorRef.Guid);

                var themeColor = theme.GetColorByRef(colorRef);
                colorField.value = themeColor.Color;

                txtName.RegisterValueChangedCallback(evt => onNameChanged?.Invoke(evt.newValue));
                colorField.RegisterValueChangedCallback(evt => onColorChanged?.Invoke(evt.newValue));
                btnDelete.clicked += OnClickColorDelete;
            }
            void OnClickColorDelete() => onDeleteRequest?.Invoke();

            public void Dispose()
            {
                root = null;
                colorField = null;
                txtName = null;
                if (btnDelete != null)
                    btnDelete.clicked -= OnClickColorDelete;
                btnDelete = null;

                onNameChanged = null;
                onColorChanged = null;
                onDeleteRequest = null;
            }
        }
    }
}