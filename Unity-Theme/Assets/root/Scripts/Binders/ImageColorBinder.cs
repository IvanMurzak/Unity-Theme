using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds a theme color to the Image component's color property.
    /// </summary>
    [AddComponentMenu("Theme/Image Color Binder")]
    public class ImageColorBinder : GenericColorBinder<Image>
    {
        protected override void SetColor(Image target, Color color)
            => target.color = color;

        protected override Color? GetColor(Image target)
            => target.color;
    }
}