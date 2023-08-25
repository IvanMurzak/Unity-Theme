using UnityEngine;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/SpriteRenderer Color Binder")]
    public class SpriteRendererColorBinder : BaseColorBinder
    {
        [SerializeField] SpriteRenderer spriteRenderer;

        protected override void Awake()
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}