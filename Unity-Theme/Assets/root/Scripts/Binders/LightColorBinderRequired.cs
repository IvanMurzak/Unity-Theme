using UnityEngine;

namespace Unity.Theme.Binders
{
    [RequireComponent(typeof(Light))]
    [AddComponentMenu("Theme/Light Color Binder (Required)")]
    /// <summary>
    /// Binds a theme color to the Light component's color property.
    /// This "Required" variant enforces the presence of a Light component.
    /// </summary>
    public class LightColorBinderRequired : LightColorBinder
    {
    }
}
