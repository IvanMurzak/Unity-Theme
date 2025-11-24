using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class BaseColorBinder : LogableMonoBehaviour
    {
        /// <summary>
        /// Set color by name from current theme
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SetColorByName(string name) => SetColor(Theme.Instance?.GetColorByName(name));

        /// <summary>
        /// Set color by GUID from current theme
        /// </summary>
        /// <param name="colorGuid"></param>
        /// <returns></returns>
        public bool SetColorByGuid(string colorGuid) => SetColor(Theme.Instance?.GetColorByGuid(colorGuid));

        /// <summary>
        /// Set color data reference
        /// </summary>
        /// <param name="colorData"></param>
        /// <returns></returns>
        public bool SetColor(ColorDataRef colorData) => SetColorByGuid(colorData.Guid);

        /// <summary>
        /// Set color data
        /// </summary>
        /// <param name="colorData"></param>
        /// <returns></returns>
        public bool SetColor(ColorData colorData) => SetColorInternal(colorData);

        /// <summary>
        /// Set color with alpha override
        /// </summary>
        /// <param name="overrideAlpha">If true - override the original alpha color with new alpha value. If false - disable alpha override.</param>
        /// <param name="alpha">alpha value in the range from 0.0 to 1.0</param>
        /// <returns>Operation success</returns>
        public bool SetAlphaOverride(bool overrideAlpha, float alpha = 1.0f)
        {
            if (data.overrideAlpha == overrideAlpha && Mathf.Abs(data.alpha - alpha) < Mathf.Epsilon)
                return true; // skip if the same alpha

            data.overrideAlpha = overrideAlpha;
            data.alpha = alpha;

            LogTrace("SetAlpha: '<b>{0}</b>' {1}", data.ColorName, alpha);

            InvalidateColor(Theme.Instance?.CurrentTheme);
            return true;
        }

        /// <summary>
        /// Get alpha override status
        /// </summary>
        /// <returns>Returns true if alpha is overridden, otherwise - false</returns>
        public bool IsAlphaOverridden() => data.overrideAlpha;

        /// <summary>
        /// Get alpha override value
        /// </summary>
        /// <returns>Alpha value [0.0 - 1.0]. Returns 1.0 if 'overrideAlpha' is false</returns>
        public float GetAlphaOverrideValue() => data.overrideAlpha ? data.alpha : 1.0f;
    }
}