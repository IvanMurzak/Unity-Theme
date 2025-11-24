using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Custom property drawer for FixedMultiColorBinderEntries.
    /// Displays the entries array without allowing size modification.
    /// </summary>
    [CustomPropertyDrawer(typeof(FixedMultiColorBinderEntries))]
    public class FixedMultiColorBinderEntriesDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            // Get the entries array property
            var entriesProperty = property.FindPropertyRelative(nameof(FixedMultiColorBinderEntries.colorBindings));

            if (entriesProperty == null || !entriesProperty.isArray)
            {
                var errorLabel = new Label("Error: Could not find entries array");
                errorLabel.style.color = UnityEngine.Color.red;
                root.Add(errorLabel);
                return root;
            }

            // Add each entry to the foldout
            for (int i = 0; i < entriesProperty.arraySize; i++)
            {
                var elementProperty = entriesProperty.GetArrayElementAtIndex(i);

                // Create a PropertyField for each entry
                // This will use the MultiColorBinderEntryDrawer for rendering
                var entryField = new PropertyField(elementProperty, $"Color Binding {i}");
                entryField.Bind(property.serializedObject);

                root.Add(entryField);
            }

            return root;
        }
    }
}
