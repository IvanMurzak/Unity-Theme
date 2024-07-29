using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/TextMeshPro Color Binder")]
    public class TextMeshProColorBinder : BaseColorBinder
    {
        [SerializeField] TextMeshProUGUI textMeshPro;

        protected override IEnumerable<Object> ColorTargets { get { yield return textMeshPro; } }

        protected override void Awake()
        {
            if (textMeshPro == null) textMeshPro = GetComponent<TextMeshProUGUI>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            if (textMeshPro == null)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"TextMeshPro not found at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
            textMeshPro.color = color;
        }
        protected override Color? GetColor() => textMeshPro?.color;
    }
}