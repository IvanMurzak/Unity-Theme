using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace Unity.Theme.Binders
{
    public class TextMeshProColorBinder : BaseColorBinder
    {
        [SerializeField, Required] TextMeshProUGUI textMeshPro;
        protected override void SetColor(Color color)
        {
            textMeshPro.color = color;
        }
    }
}