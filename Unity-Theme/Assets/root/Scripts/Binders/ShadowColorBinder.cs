using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds a theme color to the Shadow component's effectColor property.
    /// </summary>
    [AddComponentMenu("Theme/Shadow Color Binder")]
    public class ShadowColorBinder : GenericColorBinder<Shadow>
    {
        protected override void SetColor(Shadow target, Color color)
            => target.effectColor = color;

        protected override Color? GetColor(Shadow target)
            => target.effectColor;
    }
}