using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Renderer's material color
    /// Supports binding the main material color property or a custom shader property
    /// Requires a Renderer component to be present
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("Theme/Renderer Color Binder (Required)")]
    public class RendererColorBinderRequired : RendererColorBinder
    {
    }
}
