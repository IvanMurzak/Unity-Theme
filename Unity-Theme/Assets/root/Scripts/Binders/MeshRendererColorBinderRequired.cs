using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a MeshRenderer's material color
    /// Supports binding the main material color property or a custom shader property
    /// Requires a MeshRenderer component to be present
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    [AddComponentMenu("Theme/MeshRenderer Color Binder (Required)")]
    public class MeshRendererColorBinderRequired : MeshRendererColorBinder
    {
    }
}
