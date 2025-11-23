using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Unity Button's ColorBlock
    /// Supports binding all 5 button states: Normal, Highlighted, Pressed, Selected, and Disabled
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonColorBinder : SelectableColorBinder
    {
    }
}
