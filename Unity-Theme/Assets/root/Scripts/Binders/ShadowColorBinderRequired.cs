using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds a theme color to the Shadow component's effectColor property.
    /// Requires a Shadow component to be attached to the GameObject.
    /// </summary>
    [RequireComponent(typeof(Shadow))]
    [AddComponentMenu("Theme/Shadow Color Binder (Required)")]
    public class ShadowColorBinderRequired : ShadowColorBinder
    {
    }
}
