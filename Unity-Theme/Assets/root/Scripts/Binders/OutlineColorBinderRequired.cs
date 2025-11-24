using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// A Theme binder that applies a color to the Outline component.
    /// This "Required" variant enforces the presence of an Outline component.
    /// </summary>
    [RequireComponent(typeof(Outline))]
    [AddComponentMenu("Theme/Outline Color Binder (Required)")]
    public class OutlineColorBinderRequired : OutlineColorBinder
    {
    }
}
