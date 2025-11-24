using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Unity Selectable's ColorBlock
    /// Supports binding all 5 states: Normal, Highlighted, Pressed, Selected, and Disabled
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class SelectableColorBinder : GenericMultiColorBinder<Selectable>
    {
        // Color entry indices for Selectable's ColorBlock
        private const int NORMAL_INDEX = 0;
        private const int HIGHLIGHTED_INDEX = 1;
        private const int PRESSED_INDEX = 2;
        private const int SELECTED_INDEX = 3;
        private const int DISABLED_INDEX = 4;

        protected override void Awake()
        {
            // Initialize color entries if not already set
            if (colorEntries == null || colorEntries.Length != 5)
            {
                var entries = new MultiColorBinderEntry[5];
                entries[NORMAL_INDEX] = new MultiColorBinderEntry("Normal");
                entries[HIGHLIGHTED_INDEX] = new MultiColorBinderEntry("Highlighted");
                entries[PRESSED_INDEX] = new MultiColorBinderEntry("Pressed");
                entries[SELECTED_INDEX] = new MultiColorBinderEntry("Selected");
                entries[DISABLED_INDEX] = new MultiColorBinderEntry("Disabled");
                colorEntries = new FixedMultiColorBinderEntries(entries);
            }

            base.Awake();
        }

        protected override void SetColors(Selectable targetComponent, Color[] colors)
        {
            if (targetComponent.IsNull())
            {
                LogError("Selectable target is null");
                return;
            }

            if (colors == null || colors.Length != 5)
            {
                LogError("Invalid colors array. Expected 5 colors for Selectable states.");
                return;
            }

            // Get the current ColorBlock to preserve non-color properties
            var colorBlock = targetComponent.colors;

            // Update all color properties
            colorBlock.normalColor = colors[NORMAL_INDEX];
            colorBlock.highlightedColor = colors[HIGHLIGHTED_INDEX];
            colorBlock.pressedColor = colors[PRESSED_INDEX];
            colorBlock.selectedColor = colors[SELECTED_INDEX];
            colorBlock.disabledColor = colors[DISABLED_INDEX];

            // Apply the modified ColorBlock back to the button
            targetComponent.colors = colorBlock;
        }

        protected override Color[] GetColors(Selectable target)
        {
            if (target.IsNull())
                return new Color[5];

            var colorBlock = target.colors;
            return new Color[]
            {
                colorBlock.normalColor,
                colorBlock.highlightedColor,
                colorBlock.pressedColor,
                colorBlock.selectedColor,
                colorBlock.disabledColor
            };
        }
    }
}
