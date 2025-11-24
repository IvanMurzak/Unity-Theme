using UnityEngine;
using UnityEngine.U2D;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a SpriteShapeRenderer's color property.
    /// </summary>
    [AddComponentMenu("Theme/SpriteShapeRenderer Color Binder")]
    public class SpriteShapeRendererColorBinder : GenericColorBinder<SpriteShapeRenderer>
    {
        protected override void SetColor(SpriteShapeRenderer target, Color color)
            => target.color = color;

        protected override Color? GetColor(SpriteShapeRenderer target)
            => target.color;
    }
}