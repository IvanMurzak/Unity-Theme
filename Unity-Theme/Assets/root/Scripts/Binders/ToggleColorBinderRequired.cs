using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Unity Toggle's ColorBlock
    /// Supports binding all 5 toggle states: Normal, Highlighted, Pressed, Selected, and Disabled
    /// Requires a Toggle component to be present
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("Theme/Toggle Color Binder (Required)")]
    public class ToggleColorBinderRequired : ToggleColorBinder
    {
    }
}
