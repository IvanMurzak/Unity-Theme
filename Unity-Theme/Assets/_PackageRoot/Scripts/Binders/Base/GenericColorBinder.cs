using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract class GenericColorBinder<T> : BaseColorBinder where T : Component
    {
        [SerializeField] T target;

        /// <summary>
        /// Target component to apply color
        /// </summary>
        public T Target
        {
            get => target;
            set
            {
                if (target == value)
                    return; // skip if same value

                target = value;
                InvalidateColor(Theme.Instance?.CurrentTheme);
            }
        }

        /// <summary>
        /// List of targets components to apply color
        /// It is used to mark as dirty when color is changed, needed for prefab and scene editing
        /// </summary>
        protected override IEnumerable<Object> ColorTargets { get { yield return target; } }

        protected override void Awake()
        {
            if (ReferenceEquals(target, null))
                target = GetComponent<T>();
            base.Awake();
        }
        protected override bool InternalSetColor(Color color)
        {
            if (target.IsNull())
            {
                if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                    Debug.LogError($"[Theme] {typeof(T).Name} not found at <b>{GameObjectPath()}</b>", gameObject);
                return false;
            }
            return base.InternalSetColor(color);
        }
        protected override bool CanApplyColor() => target.IsNotNull();
        protected override void SetColorInternal(Color color) => SetColor(target, color);

        /// <summary>
        /// Get color from target component
        /// </summary>
        /// <returns>Returns nullable Color</returns>
        public override Color? GetColor() => GetColor(target);

        protected abstract void SetColor(T target, Color color);
        protected abstract Color? GetColor(T target);

    }
}