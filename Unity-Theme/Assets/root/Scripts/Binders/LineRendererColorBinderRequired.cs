using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a LineRenderer's color.
    /// Supports binding separate start and end colors.
    /// Requires a LineRenderer component to be present
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    [AddComponentMenu("Theme/LineRenderer Color Binder (Required)")]
    public class LineRendererColorBinderRequired : LineRendererColorBinder
    {
    }
}
