using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds a theme color to the Image component's color property.
    /// Requires an Image component to be attached to the GameObject.
    /// </summary>
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Theme/Image Color Binder (Required)")]
    public class ImageColorBinderRequired : ImageColorBinder
    {
    }
}
