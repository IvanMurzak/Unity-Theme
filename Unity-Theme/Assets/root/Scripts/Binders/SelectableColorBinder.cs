using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Unity Selectable's ColorBlock
    /// Supports binding all 5 states: Normal, Highlighted, Pressed, Selected, and Disabled
    /// </summary>
    [AddComponentMenu("Theme/Selectable Color Binder")]
    public class SelectableColorBinder : GenericMultiColorBinder<Selectable>
    {
        // Color entry indices for Selectable's ColorBlock
        private const int NORMAL_INDEX = 0;
        private const int HIGHLIGHTED_INDEX = 1;
        private const int PRESSED_INDEX = 2;
        private const int SELECTED_INDEX = 3;
        private const int DISABLED_INDEX = 4;

        /// <summary>
        /// Defines the color entry labels for Selectable's ColorBlock states
        /// </summary>
        protected override string[] ColorEntries => new string[]
        {
            "Normal",
            "Highlighted",
            "Pressed",
            "Selected",
            "Disabled"
        };

        protected override void SetColors(Selectable targetComponent, Color[] colors)
        {
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
