namespace Unity.Theme.Binders
{
    public abstract partial class BaseMultiColorBinder : LogableMonoBehaviour
    {
#if UNITY_EDITOR
        protected virtual void OnValidate() => UnityEditor.EditorApplication.delayCall += Validate;
#endif

        protected virtual void Validate()
        {
            if (this.IsNull())
                return;

            bool hasErrors = false;

            // Validate all color entries
            for (int i = 0; i < colorEntries.Length; i++)
            {
                var entry = colorEntries[i];
                if (entry == null)
                {
#if UNITY_EDITOR
                    LogError("Color entry at index {0} is <b><color=red>null</color></b>", i);
#else
                    LogError("Color entry at index {0} is null", i);
#endif
                    hasErrors = true;
                    continue;
                }

                if (entry.colorData == null)
                {
#if UNITY_EDITOR
                    LogError("Color data for entry '{0}' at index {1} is <b><color=red>null</color></b>", entry.label, i);
#else
                    LogError("Color data for entry '{0}' at index {1} is null", entry.label, i);
#endif
                    hasErrors = true;
                    continue;
                }

                if (string.IsNullOrEmpty(entry.colorData.colorGuid))
                {
#if UNITY_EDITOR
                    LogError("Color GUID for entry '{0}' at index {1} is <b><color=red>null</color></b>", entry.label, i);
#else
                    LogError("Color GUID for entry '{0}' at index {1} is null", entry.label, i);
#endif
                    hasErrors = true;
                    continue;
                }

                if (!entry.colorData.IsConnected)
                {
                    LogError("Color with GUID='{0}' for entry '{1}' at index {2} not found in database", entry.colorData.colorGuid, entry.label, i);
                    hasErrors = true;
                }
            }

            if (hasErrors)
                return;

#if UNITY_EDITOR
            InvalidateColors(Theme.Instance.CurrentTheme);
#endif
        }
    }
}
