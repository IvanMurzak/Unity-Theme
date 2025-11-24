using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds a theme color to the Light component's color property.
    /// This "Required" variant enforces the presence of a Light component.
    /// </summary>
    [RequireComponent(typeof(Light))]
    [AddComponentMenu("Theme/Light Color Binder (Required)")]
    public class LightColorBinderRequired : LightColorBinder
    {
    }
}
