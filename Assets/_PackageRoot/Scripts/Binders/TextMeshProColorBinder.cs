using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/TextMeshPro Color Binder")]
    public class TextMeshProColorBinder : BaseColorBinder
    {
        [SerializeField, Required] TextMeshProUGUI textMeshPro;

        protected override void Awake()
        {
            if (textMeshPro == null) textMeshPro = GetComponent<TextMeshProUGUI>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            textMeshPro.color = color;
        }
    }
}