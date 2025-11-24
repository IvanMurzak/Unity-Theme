using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a SkinnedMeshRenderer's material color
    /// Supports binding the main material color property or a custom shader property
    /// Requires a SkinnedMeshRenderer component to be present
    /// </summary>
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [AddComponentMenu("Theme/SkinnedMeshRenderer Color Binder (Required)")]
    public class SkinnedMeshRendererColorBinderRequired : SkinnedMeshRendererColorBinder
    {
    }
}
