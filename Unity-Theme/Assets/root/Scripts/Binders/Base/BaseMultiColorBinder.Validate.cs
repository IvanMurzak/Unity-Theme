using System.Text;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class BaseMultiColorBinder : MonoBehaviour
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
                    if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                        Debug.LogError($"[Theme] Color entry at index {i} is <b><color=red>null</color></b> at <b>{GameObjectPath()}</b>", gameObject);
                    hasErrors = true;
                    continue;
                }

                if (entry.colorData == null)
                {
                    if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                        Debug.LogError($"[Theme] Color data for entry '{entry.label}' at index {i} is <b><color=red>null</color></b> at <b>{GameObjectPath()}</b>", gameObject);
                    hasErrors = true;
                    continue;
                }

                if (string.IsNullOrEmpty(entry.colorData.colorGuid))
                {
                    if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                        Debug.LogError($"[Theme] Color GUID for entry '{entry.label}' at index {i} is <b><color=red>null</color></b> at <b>{GameObjectPath()}</b>", gameObject);
                    hasErrors = true;
                    continue;
                }

                if (!entry.colorData.IsConnected)
                {
                    if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                        Debug.LogError($"[Theme] Color with GUID='{entry.colorData.colorGuid}' for entry '{entry.label}' at index {i} not found in database at <b>{GameObjectPath()}</b>", gameObject);
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
