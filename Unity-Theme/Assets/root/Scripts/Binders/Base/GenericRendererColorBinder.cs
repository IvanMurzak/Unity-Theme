using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Renderer's material color
    /// Supports binding the main material color property or a custom shader property
    /// </summary>
    public abstract class GenericRendererColorBinder<T> : GenericMultiColorBinder<T> where T : Renderer
    {
        // Color entry index for material's main color
        protected const int MATERIAL_COLOR_INDEX = 0;

        [SerializeField]
        [Tooltip("Enable to use a custom material property name instead of the default color property")]
        protected bool useCustomProperty = false;

        [SerializeField]
        [Tooltip("Custom shader property name to bind the color to (e.g., '_BaseColor', '_EmissionColor')")]
        protected string customMaterialColorProperty = "_Color";

        /// <summary>
        /// Defines the color entry labels for Renderer's material
        /// </summary>
        protected override string[] ColorEntries => new string[]
        {
            "Material Color"
        };

        protected override void SetColors(T targetComponent, Color[] colors)
        {
            if (targetComponent.material != null)
            {
                if (useCustomProperty && !string.IsNullOrEmpty(customMaterialColorProperty))
                {
                    targetComponent.material.SetColor(customMaterialColorProperty, colors[MATERIAL_COLOR_INDEX]);
                }
                else
                {
                    targetComponent.material.color = colors[MATERIAL_COLOR_INDEX];
                }
            }
        }

        protected override Color[] GetColors(T target)
        {
            if (target.material != null)
            {
                Color color;
                if (useCustomProperty && !string.IsNullOrEmpty(customMaterialColorProperty))
                {
                    color = target.material.HasProperty(customMaterialColorProperty)
                        ? target.material.GetColor(customMaterialColorProperty)
                        : Color.white;
                }
                else
                {
                    color = target.material.color;
                }

                return new Color[] { color };
            }

            return new Color[] { Color.white };
        }
    }
}
