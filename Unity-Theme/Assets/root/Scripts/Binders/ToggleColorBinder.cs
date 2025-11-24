using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a Unity Toggle's ColorBlock
    /// Supports binding all 5 toggle states: Normal, Highlighted, Pressed, Selected, and Disabled
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class ToggleColorBinder : SelectableColorBinder
    {
    }
}
