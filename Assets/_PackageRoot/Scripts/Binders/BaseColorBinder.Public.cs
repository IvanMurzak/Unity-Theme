using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class BaseColorBinder : MonoBehaviour
    {
        public bool SetColorByName(string name) => SetColor(Theme.Instance?.GetColorByName(name));
        public bool SetColorByGuid(string colorGuid) => SetColor(Theme.Instance?.GetColorByGuid(colorGuid));
        public bool SetColor(ColorDataRef colorData) => SetColorByGuid(colorData.Guid);
        public bool SetColor(ColorData colorData)
        {
            if (colorData == null)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color is null. Can't set it as a color for the binder", gameObject);
                return false;
            }
            data.colorGuid = colorData.Guid;
            var color = GetColor(colorData);

            if (Theme.Instance?.debugLevel <= DebugLevel.Log)
                Debug.Log($"SetColor: '<b>{data.ColorName}</b>' {color.ToHexRGBA()} at <b>{GameObjectPath()}</b>", gameObject);

            SetColor(color);
            return true;
        }
    }
}