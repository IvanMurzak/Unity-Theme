using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Unity Button's ColorBlock
    /// Supports binding all 5 button states: Normal, Highlighted, Pressed, Selected, and Disabled
    /// Requires a Button component to be present
    /// </summary>
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Theme/Button Color Binder (Required)")]
    public class ButtonColorBinderRequired : ButtonColorBinder
    {
    }
}
