using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a SkinnedMeshRenderer's material color
    /// Supports binding the main material color property or a custom shader property
    /// </summary>
    [AddComponentMenu("Theme/SkinnedMeshRenderer Color Binder")]
    public class SkinnedMeshRendererColorBinder : GenericRendererColorBinder<SkinnedMeshRenderer>
    {
    }
}
