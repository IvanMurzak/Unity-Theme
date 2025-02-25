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
            if (data.colorGuid == colorData.Guid)
                return true; // skip if the same color

            data.colorGuid = colorData.Guid;
            var color = GetTargetColor(colorData);

            if (Theme.Instance?.debugLevel <= DebugLevel.Log)
                Debug.Log($"SetColor: '<b>{data.ColorName}</b>' {color.ToHexRGBA()} at <b>{GameObjectPath()}</b>", gameObject);

            SetColor(color);
            SetDirty();
            return true;
        }
    }
}