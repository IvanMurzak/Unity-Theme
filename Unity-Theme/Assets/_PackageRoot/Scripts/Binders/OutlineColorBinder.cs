using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/Outline Color Binder")]
    public class OutlineColorBinder : GenericColorBinder<Outline>
    {
        protected override void SetColor(Outline target, Color color)
            => target.effectColor = color;

        protected override Color? GetColor(Outline target)
            => target.effectColor;
    }
}