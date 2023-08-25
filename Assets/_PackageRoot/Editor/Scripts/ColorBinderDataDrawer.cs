using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.Theme.Binders;

namespace Unity.Theme.Editor
{
    [CustomPropertyDrawer(typeof(ColorBinderData))]
    public class ColorBinderDataDrawer : PropertyDrawer
    {
        const string templateGuid = "7bc7f57ecc1dcb54ebd343051d02f17b";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(templateGuid)).Instantiate();
            var toggleOverrideAlpha = root.Query<Toggle>("toggleOverrideAlpha").First();
            var dropdownColor = root.Query<DropdownField>("dropdownColor").First();
            var btnOpenConfig = root.Query<Button>("btnOpenConfig").First();
            var sliderAlpha = root.Query<Slider>("sliderAlpha").First();
            var colorFill = root.Query<VisualElement>("colorFill").Last();

            var colorGuid = property.FindPropertyRelative("colorGuid");
            var overrideAlpha = property.FindPropertyRelative("overrideAlpha");
            var alpha = property.FindPropertyRelative("alpha");

            dropdownColor.choices = ThemeDatabase.Instance?.ColorNames?.ToList() ?? new List<string>() { "error" };
            dropdownColor.value = ThemeDatabase.Instance?.GetColorName(colorGuid.stringValue);
            toggleOverrideAlpha.value = overrideAlpha.boolValue;
            sliderAlpha.visible = overrideAlpha.boolValue;
            sliderAlpha.value = alpha.floatValue;

            UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);

            dropdownColor.RegisterValueChangedCallback(evt =>
            {
                var guid = ThemeDatabase.Instance?.GetColorByName(evt.newValue)?.Guid;
                colorGuid.stringValue = guid;
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
                colorGuid.serializedObject.ApplyModifiedProperties();
            });

            toggleOverrideAlpha.RegisterValueChangedCallback(evt =>
            {
                overrideAlpha.boolValue = evt.newValue;
                sliderAlpha.visible = evt.newValue;
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
                overrideAlpha.serializedObject.ApplyModifiedProperties();
            });

            sliderAlpha.RegisterValueChangedCallback(evt =>
            {
                alpha.floatValue = evt.newValue;
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
                alpha.serializedObject.ApplyModifiedProperties();
            });

            btnOpenConfig.clicked += () => ThemeDatabaseEditor.ShowWindow();

            colorFill.BringToFront();

            return root;
        }
        void UpdateColorFill(VisualElement colorFill, string colorGuid, float alpha)
        {
            var color = ThemeDatabase.Instance?.GetColorByGuid(colorGuid)?.color ?? ThemeDatabase.DefaultColor;
            color.a = alpha;
            colorFill.style.unityBackgroundImageTintColor = new StyleColor(color);
        }
    }
}