using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract class GenericMultiColorBinder<T> : BaseMultiColorBinder where T : Component
    {
        [SerializeField] T target;

        /// <summary>
        /// Target component to apply colors
        /// </summary>
        public T Target
        {
            get => target;
            set
            {
                if (target == value)
                    return; // skip if same value

                target = value;
                InvalidateColors(Theme.Instance?.CurrentTheme);
            }
        }

        /// <summary>
        /// List of targets components to apply colors
        /// It is used to mark as dirty when colors are changed, needed for prefab and scene editing
        /// </summary>
        protected override IEnumerable<Object> ColorTargets { get { yield return target; } }

        protected override void Awake()
        {
            if (ReferenceEquals(target, null))
                target = GetComponent<T>();
            base.Awake();
        }

        protected override bool InternalSetColors(Color[] colors)
        {
            if (target.IsNull())
            {
                LogError($"{typeof(T).Name} not found");
                return false;
            }
            return base.InternalSetColors(colors);
        }

        protected override bool CanApplyColors() => target.IsNotNull();
        protected override bool SetColorsInternal(Color[] colors)
        {
            if (target.IsNull())
            {
                LogError("{0} target is null", GetType().Name);
                return false;
            }
            SetColors(target, colors);
            return true;
        }

        /// <summary>
        /// Get colors from target component
        /// </summary>
        /// <returns>Returns array of colors</returns>
        public override Color[] GetColors()
        {
            if (target.IsNull())
                return new Color[ColorEntries.Length];

            return GetColors(target);
        }

        /// <summary>
        /// Set colors to target component
        /// </summary>
        /// <param name="targetComponent">Target component</param>
        /// <param name="colors">Array of colors to apply</param>
        protected abstract void SetColors(T targetComponent, Color[] colors);

        /// <summary>
        /// Get colors from target component
        /// </summary>
        /// <param name="targetComponent">Target component</param>
        /// <returns>Array of colors from the target</returns>
        protected abstract Color[] GetColors(T targetComponent);
    }
}