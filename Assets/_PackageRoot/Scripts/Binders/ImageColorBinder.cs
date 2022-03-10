using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/Image Color Binder")]
    public class ImageColorBinder : BaseColorBinder
    {
        [SerializeField, Required] Image image;

        protected override void Awake()
        {
            if (image == null) image = GetComponent<Image>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            image.color = color;
        }
    }
}