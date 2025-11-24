using UnityEngine;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/Light Color Binder")]
    public class LightColorBinder : GenericColorBinder<Light>
    {
        protected override void SetColor(Light target, Color color)
            => target.color = color;

        protected override Color? GetColor(Light target)
            => target.color;
    }
}