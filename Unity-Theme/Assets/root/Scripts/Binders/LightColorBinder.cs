using UnityEngine;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/Light Color Binder")]
    /// <summary>
    /// Binds theme colors to a Light's color.
    /// </summary>
    public class LightColorBinder : GenericColorBinder<Light>
        protected override void SetColor(Light target, Color color)
            => target.color = color;

        protected override Color? GetColor(Light target)
            => target.color;
    }
}