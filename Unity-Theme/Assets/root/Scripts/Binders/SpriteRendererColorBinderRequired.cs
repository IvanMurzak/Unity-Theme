using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a SpriteRenderer's color property.
    /// Requires a SpriteRenderer component to be attached to the GameObject.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Theme/SpriteRenderer Color Binder (Required)")]
    public class SpriteRendererColorBinderRequired : SpriteRendererColorBinder
    {
    }
}
