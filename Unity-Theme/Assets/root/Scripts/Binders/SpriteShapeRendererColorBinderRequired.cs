using UnityEngine;
using UnityEngine.U2D;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a SpriteShapeRenderer's color property.
    /// Requires a SpriteShapeRenderer component to be attached to the GameObject.
    /// </summary>
    [RequireComponent(typeof(SpriteShapeRenderer))]
    [AddComponentMenu("Theme/SpriteShapeRenderer Color Binder (Required)")]
    public class SpriteShapeRendererColorBinderRequired : SpriteShapeRendererColorBinder
    {
    }
}
