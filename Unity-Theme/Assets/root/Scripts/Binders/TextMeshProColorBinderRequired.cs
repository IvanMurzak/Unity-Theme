using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a TextMeshProUGUI's color property.
    /// Requires a TextMeshProUGUI component to be attached to the GameObject.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu("Theme/TextMeshPro Color Binder (Required)")]
    public class TextMeshProColorBinderRequired : TextMeshProColorBinder
    {
    }
}
