using UnityEngine;
using UnityEngine.U2D;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/SpriteShapeRenderer Color Binder")]
    public class SpriteShapeRendererColorBinder : GenericColorBinder<SpriteShapeRenderer>
    {
        protected override void SetColor(SpriteShapeRenderer target, Color color)
            => target.color = color;

        protected override Color? GetColor(SpriteShapeRenderer target)
            => target.color;
    }
}