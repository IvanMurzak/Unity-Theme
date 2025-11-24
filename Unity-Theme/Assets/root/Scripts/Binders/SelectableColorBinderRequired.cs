using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Unity Selectable's ColorBlock
    /// Supports binding all 5 states: Normal, Highlighted, Pressed, Selected, and Disabled
    /// Requires a Selectable component to be present
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    [AddComponentMenu("Theme/Selectable Color Binder (Required)")]
    public class SelectableColorBinderRequired : SelectableColorBinder
    {
    }
}
