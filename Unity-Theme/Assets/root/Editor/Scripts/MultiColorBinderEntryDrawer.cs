using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace Unity.Theme.Editor
{
    [CustomPropertyDrawer(typeof(Binders.MultiColorBinderEntry), true)]
    public class MultiColorBinderEntryDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            root.style.marginBottom = 4;

            // Add a separator line
            var separator = new VisualElement();
            separator.style.height = 1;
            separator.style.backgroundColor = new StyleColor(new Color(0.5f, 0.5f, 0.5f, 0.3f));
            separator.style.marginTop = 4;
            separator.style.marginBottom = 10;
            root.Add(separator);

            var labelProperty = property.FindPropertyRelative(nameof(Binders.MultiColorBinderEntry.label));
            var colorDataProperty = property.FindPropertyRelative(nameof(Binders.MultiColorBinderEntry.colorData));

            // Create a container for the label
            var labelContainer = new VisualElement();
            labelContainer.style.flexDirection = FlexDirection.Row;

            // Create label field (readonly)
            var labelField = new TextField("Label");
            labelField.value = labelProperty.stringValue;
            labelField.isReadOnly = true;
            labelField.style.flexGrow = 1;

            labelContainer.Add(labelField);

            // Add the label container to root
            root.Add(labelContainer);

            // Create a property field for the ColorBinderData
            // This will automatically use the ColorBinderDataDrawer
            var colorDataField = new PropertyField(colorDataProperty, "Color");
            colorDataField.Bind(property.serializedObject);

            root.Add(colorDataField);

            return root;
        }
    }
}
