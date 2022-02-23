using Sirenix.OdinInspector;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public class SpriteRendererColorBinder : BaseColorBinder
    {
        [SerializeField, Required] SpriteRenderer spriteRenderer;
        protected override void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}