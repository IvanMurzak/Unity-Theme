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
            var sliderAlpha = root.Query<Slider>("sliderAlpha").First();
            var colorFill = root.Query<VisualElement>("colorFill").First();

            var colorGuid = property.FindPropertyRelative("colorGuid");
            var overrideAlpha = property.FindPropertyRelative("overrideAlpha");
            var alpha = property.FindPropertyRelative("alpha");

            dropdownColor.choices = ThemeDatabaseInitializer.Config?.ColorNames?.ToList() ?? new List<string>() { "error" };
            dropdownColor.value = ThemeDatabaseInitializer.Config?.GetColorName(colorGuid.stringValue);
            toggleOverrideAlpha.value = overrideAlpha.boolValue;
            sliderAlpha.visible = overrideAlpha.boolValue;
            sliderAlpha.value = alpha.floatValue;

            UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);

            dropdownColor.RegisterValueChangedCallback(evt =>
            {
                var guid = ThemeDatabaseInitializer.Config?.GetColorByName(evt.newValue)?.Guid;
                colorGuid.stringValue = guid;
                colorGuid.serializedObject.ApplyModifiedProperties();
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
            });

            toggleOverrideAlpha.RegisterValueChangedCallback(evt =>
            {
                overrideAlpha.boolValue = evt.newValue;
                overrideAlpha.serializedObject.ApplyModifiedProperties();
                sliderAlpha.visible = evt.newValue;
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
            });

            sliderAlpha.RegisterValueChangedCallback(evt =>
            {
                alpha.floatValue = evt.newValue;
                alpha.serializedObject.ApplyModifiedProperties();
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
            });

            return root;
        }
        void UpdateColorFill(VisualElement colorFill, string colorGuid, float alpha)
        {
            var color = ThemeDatabaseInitializer.Config?.GetColorByGuid(colorGuid)?.color ?? ThemeDatabase.DefaultColor;
            color.a = alpha;
            colorFill.style.unityBackgroundImageTintColor = new StyleColor(color);
        }
    }
}