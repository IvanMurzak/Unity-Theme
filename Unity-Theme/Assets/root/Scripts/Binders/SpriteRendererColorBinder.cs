using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a SpriteRenderer's color property.
    /// </summary>
    [AddComponentMenu("Theme/SpriteRenderer Color Binder")]
    public class SpriteRendererColorBinder : GenericColorBinder<SpriteRenderer>
    {
        protected override void SetColor(SpriteRenderer target, Color color)
            => target.color = color;

        protected override Color? GetColor(SpriteRenderer target)
            => target.color;
    }
}