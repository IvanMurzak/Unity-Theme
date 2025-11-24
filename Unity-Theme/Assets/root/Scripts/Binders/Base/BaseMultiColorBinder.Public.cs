using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class BaseMultiColorBinder : LogableMonoBehaviour
    {
        /// <summary>
        /// Set color by name from current theme for a specific entry index
        /// </summary>
        /// <param name="index">Index of the color entry</param>
        /// <param name="name">Color name</param>
        /// <returns>Operation success</returns>
        public bool SetColorByName(int index, string name)
        {
            if (index < 0 || index >= colorEntries.Length)
            {
                LogError("Invalid color entry index: {0}. Valid range is 0-{1}", index, colorEntries.Length - 1);
                return false;
            }
            return SetColor(index, Theme.Instance?.GetColorByName(name));
        }

        /// <summary>
        /// Set color by GUID from current theme for a specific entry index
        /// </summary>
        /// <param name="index">Index of the color entry</param>
        /// <param name="colorGuid">Color GUID</param>
        /// <returns>Operation success</returns>
        public bool SetColorByGuid(int index, string colorGuid)
        {
            if (index < 0 || index >= colorEntries.Length)
            {
                LogError("Invalid color entry index: {0}. Valid range is 0-{1}", index, colorEntries.Length - 1);
                return false;
            }
            return SetColor(index, Theme.Instance?.GetColorByGuid(colorGuid));
        }

        /// <summary>
        /// Set color by label from current theme
        /// </summary>
        /// <param name="label">Label of the color entry (e.g., "Normal", "Highlighted")</param>
        /// <param name="name">Color name</param>
        /// <returns>Operation success</returns>
        public bool SetColorByLabel(string label, string name)
        {
            int index = GetIndexByLabel(label);
            if (index < 0)
            {
                LogError("Color entry with label '{0}' not found", label);
                return false;
            }
            return SetColorByName(index, name);
        }

        /// <summary>
        /// Set color data for a specific entry index
        /// </summary>
        /// <param name="index">Index of the color entry</param>
        /// <param name="colorData">Color data</param>
        /// <returns>Operation success</returns>
        public bool SetColor(int index, ColorData colorData)
        {
            if (index < 0 || index >= colorEntries.Length)
            {
                LogError("Invalid color entry index: {0}. Valid range is 0-{1}", index, colorEntries.Length - 1);
                return false;
            }
            if (colorData == null)
            {
                LogError("Color data is null. Can't set it for entry at index {0}", index);
                return false;
            }
            if (colorEntries[index].colorData.colorGuid == colorData.Guid)
                return true; // skip if the same color

            colorEntries[index].colorData.colorGuid = colorData.Guid;
            InvalidateColors(Theme.Instance?.CurrentTheme);
            return true;
        }

        /// <summary>
        /// Set alpha override for a specific entry index
        /// </summary>
        /// <param name="index">Index of the color entry</param>
        /// <param name="overrideAlpha">If true - override the original alpha color with new alpha value. If false - disable alpha override.</param>
        /// <param name="alpha">alpha value in the range from 0.0 to 1.0</param>
        /// <returns>Operation success</returns>
        public bool SetAlphaOverride(int index, bool overrideAlpha, float alpha = 1.0f)
        {
            if (index < 0 || index >= colorEntries.Length)
            {
                LogError("Invalid color entry index: {0}. Valid range is 0-{1}", index, colorEntries.Length - 1);
                return false;
            }

            var entry = colorEntries[index];
            if (entry.colorData.overrideAlpha == overrideAlpha && Mathf.Abs(entry.colorData.alpha - alpha) < Mathf.Epsilon)
                return true; // skip if the same alpha

            entry.colorData.overrideAlpha = overrideAlpha;
            entry.colorData.alpha = alpha;

            LogTrace($"SetAlpha for '{entry.label}': {alpha}");

            InvalidateColors(Theme.Instance?.CurrentTheme);
            return true;
        }

        /// <summary>
        /// Set alpha override for a specific entry by label
        /// </summary>
        /// <param name="label">Label of the color entry</param>
        /// <param name="overrideAlpha">If true - override the original alpha color with new alpha value. If false - disable alpha override.</param>
        /// <param name="alpha">alpha value in the range from 0.0 to 1.0</param>
        /// <returns>Operation success</returns>
        public bool SetAlphaOverrideByLabel(string label, bool overrideAlpha, float alpha = 1.0f)
        {
            int index = GetIndexByLabel(label);
            if (index < 0)
            {
                LogError("Color entry with label '{0}' not found", label);
                return false;
            }
            return SetAlphaOverride(index, overrideAlpha, alpha);
        }

        /// <summary>
        /// Get alpha override status for a specific entry index
        /// </summary>
        /// <param name="index">Index of the color entry</param>
        /// <returns>Returns true if alpha is overridden, otherwise - false</returns>
        public bool IsAlphaOverridden(int index)
        {
            if (index < 0 || index >= colorEntries.Length)
                return false;
            return colorEntries[index].colorData.overrideAlpha;
        }

        /// <summary>
        /// Get alpha override value for a specific entry index
        /// </summary>
        /// <param name="index">Index of the color entry</param>
        /// <returns>Alpha value [0.0 - 1.0]. Returns 1.0 if 'overrideAlpha' is false or index is invalid</returns>
        public float GetAlphaOverrideValue(int index)
        {
            if (index < 0 || index >= colorEntries.Length)
                return 1.0f;
            return colorEntries[index].colorData.overrideAlpha ? colorEntries[index].colorData.alpha : 1.0f;
        }

        /// <summary>
        /// Get the index of a color entry by its label
        /// </summary>
        /// <param name="label">Label to search for</param>
        /// <returns>Index of the entry, or -1 if not found</returns>
        public int GetIndexByLabel(string label)
        {
            for (int i = 0; i < colorEntries.Length; i++)
            {
                if (colorEntries[i].label == label)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Get the number of color entries
        /// </summary>
        /// <returns>Number of color entries</returns>
        public int GetColorEntryCount() => colorEntries.Length;
    }
}
