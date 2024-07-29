using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/SpriteRenderer Color Binder")]
    public class SpriteRendererColorBinder : BaseColorBinder
    {
        [SerializeField] SpriteRenderer spriteRenderer;

        protected override IEnumerable<Object> ColorTargets { get { yield return spriteRenderer; } }

        protected override void Awake()
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            if (spriteRenderer == null)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"SpriteRenderer not found at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
            spriteRenderer.color = color;
        }
        protected override Color? GetColor() => spriteRenderer?.color;
    }
}