using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Renderer's material color
    /// Supports binding the main material color property or a custom shader property
    /// </summary>
    [AddComponentMenu("Theme/Renderer Color Binder")]
    public class RendererColorBinder : GenericRendererColorBinder<Renderer>
    {
    }
}
