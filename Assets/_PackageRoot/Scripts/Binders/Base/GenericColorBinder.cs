using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract class GenericColorBinder<T> : BaseColorBinder where T : Component
    {
        [SerializeField] T target;

        public T Target
        {
            get => target;
            set
            {
                if (target == value)
                    return; // skip if same value

                target = value;
                TrySetColor(Theme.Instance?.CurrentTheme);
            }
        }

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
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"{typeof(T).Name} not found at <b>{GameObjectPath()}</b>", gameObject);
                return false;
            }
            return base.InternalSetColor(color);
        }
        protected override bool CanApplyColor() => target.IsNotNull();
        protected override void SetColor(Color color) => SetColor(target, color);
        public override Color? GetColor() => GetColor(target);

        protected abstract void SetColor(T target, Color color);
        protected abstract Color? GetColor(T target);

    }
}