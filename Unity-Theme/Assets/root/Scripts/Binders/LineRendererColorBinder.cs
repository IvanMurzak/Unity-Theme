using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a LineRenderer's color.
    /// Supports binding separate start and end colors.
    /// </summary>
    [AddComponentMenu("Theme/LineRenderer Color Binder")]
    public class LineRendererColorBinder : GenericMultiColorBinder<LineRenderer>
    {
        // Color entry indices for LineRenderer
        private const int START_INDEX = 0;
        private const int END_INDEX = 1;

        /// <summary>
        /// Defines the color entry labels for LineRenderer's color properties
        /// </summary>
        protected override string[] ColorEntries => new string[]
        {
            "Start",
            "End"
        };

        protected override void SetColors(LineRenderer targetComponent, Color[] colors)
        {
            targetComponent.startColor = colors[START_INDEX];
            targetComponent.endColor = colors[END_INDEX];
        }

        protected override Color[] GetColors(LineRenderer targetComponent)
        {
            return new Color[]
            {
                targetComponent.startColor,
                targetComponent.endColor
            };
        }
    }
}
