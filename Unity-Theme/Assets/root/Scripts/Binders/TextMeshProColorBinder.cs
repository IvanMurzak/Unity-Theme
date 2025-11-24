using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/TextMeshPro Color Binder")]
    public class TextMeshProColorBinder : GenericColorBinder<TMP_Text>
    {
        protected override void SetColor(TMP_Text target, Color color)
            => target.color = color;

        protected override Color? GetColor(TMP_Text target)
            => target.color;
    }
}