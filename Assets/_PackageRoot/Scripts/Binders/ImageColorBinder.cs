using UnityEngine;
using UnityEngine.UI;

namespace Unity.Theme.Binders
{
    [AddComponentMenu("Theme/Image Color Binder")]
    public class ImageColorBinder : BaseColorBinder
    {
        [SerializeField] Image image;

        protected override void Awake()
        {
            if (image == null) image = GetComponent<Image>();
            base.Awake();
        }
        protected override void SetColor(Color color)
        {
            if (image == null)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Image not found at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
            image.color = color;
        }
    }
}