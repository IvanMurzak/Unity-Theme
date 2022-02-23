using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    public class ImageColorBinder : BaseColorBinder
    {
        [SerializeField, Required] Image image;
        protected override void SetColor(Color color)
        {
            image.color = color;
        }
    }
}