using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Theme/Image Color Binder (Required)")]
    /// <summary>
    /// Binds a theme color to the Image component's color property.
    /// Requires an Image component to be attached to the GameObject.
    /// </summary>
    public class ImageColorBinderRequired : ImageColorBinder
    {
    }
}
