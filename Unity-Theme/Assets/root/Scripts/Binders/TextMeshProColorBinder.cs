using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Binds theme colors to a TextMeshProUGUI's color property.
    /// </summary>
    [AddComponentMenu("Theme/TextMeshPro Color Binder")]
    public class TextMeshProColorBinder : GenericColorBinder<TextMeshProUGUI>
    {
        protected override void SetColor(TextMeshProUGUI target, Color color)
            => target.color = color;

        protected override Color? GetColor(TextMeshProUGUI target)
            => target.color;
    }
}