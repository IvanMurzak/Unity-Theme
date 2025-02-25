using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/Shadow Color Binder")]
    public class ShadowColorBinder : BaseColorBinder
    {
        [SerializeField] Shadow target;

        protected override IEnumerable<Object> ColorTargets { get { yield return target; } }

        protected override void Awake()
        {
            if (ReferenceEquals(target, null))
                target = GetComponent<Shadow>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            if (target.IsNull())
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Target not found at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
            target.effectColor = color;
        }
        protected override Color? GetColor()
        {
            if (target.IsNull())
                return null;

            return target.effectColor;
        }
    }
}