using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Light's color.
    /// </summary>
    [AddComponentMenu("Theme/Light Color Binder")]
    public class LightColorBinder : GenericColorBinder<Light>
    {
        protected override void SetColor(Light target, Color color)
            => target.color = color;

        protected override Color? GetColor(Light target)
            => target.color;
    }
}