using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/TextMeshPro Color Binder")]
    public class TextMeshProColorBinder : GenericColorBinder<TextMeshProUGUI>
    {
        protected override void SetColor(TextMeshProUGUI target, Color color)
            => target.color = color;

        protected override Color? GetColor(TextMeshProUGUI target)
            => target.color;
    }
}