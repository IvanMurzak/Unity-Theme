using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;

namespace Unity.Theme.Editor
{
    [CustomPropertyDrawer(typeof(Binders.ColorBinderData), true)]
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

            var colorGuid = property.FindPropertyRelative(nameof(Binders.ColorBinderData.colorGuid));
            var overrideAlpha = property.FindPropertyRelative(nameof(Binders.ColorBinderData.overrideAlpha));
            var alpha = property.FindPropertyRelative(nameof(Binders.ColorBinderData.alpha));

            dropdownColor.choices = Theme.Instance?.ColorNames?.ToList() ?? new List<string>() { "error" };
            dropdownColor.value = Theme.Instance?.GetColorName(colorGuid.stringValue);
            toggleOverrideAlpha.value = overrideAlpha.boolValue;
            sliderAlpha.visible = overrideAlpha.boolValue;
            sliderAlpha.value = alpha.floatValue;

            UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);

            dropdownColor.SubscribeOnValueChanged(root, evt =>
            {
                var guid = Theme.Instance?.GetColorByName(evt.newValue)?.Guid;
                colorGuid.stringValue = guid;
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
                colorGuid.serializedObject.ApplyModifiedProperties();
            });

            toggleOverrideAlpha.SubscribeOnValueChanged(root, evt =>
            {
                overrideAlpha.boolValue = evt.newValue;
                sliderAlpha.visible = evt.newValue;
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
                overrideAlpha.serializedObject.ApplyModifiedProperties();
            });

            sliderAlpha.SubscribeOnValueChanged(root, evt =>
            {
                alpha.floatValue = evt.newValue;
                UpdateColorFill(colorFill, colorGuid.stringValue, overrideAlpha.boolValue ? alpha.floatValue : 1f);
                alpha.serializedObject.ApplyModifiedProperties();
            });

            btnOpenConfig.clicked += ThemeWindowEditor.ShowWindowVoid;
            root.RegisterCallback<DetachFromPanelEvent>(evt => btnOpenConfig.clicked -= ThemeWindowEditor.ShowWindowVoid);

            colorFill.BringToFront();

            return root;
        }
        void UpdateColorFill(VisualElement colorFill, string colorGuid, float alpha)
        {
            var color = Theme.Instance?.GetColorByGuid(colorGuid)?.Color ?? Theme.DefaultColor;
            color.a = alpha;
            colorFill.style.unityBackgroundImageTintColor = new StyleColor(color);
        }
    }
}