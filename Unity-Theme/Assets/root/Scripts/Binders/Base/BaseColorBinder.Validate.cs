using System.Text;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class BaseColorBinder : MonoBehaviour
    {
#if UNITY_EDITOR
        protected virtual void OnValidate() => UnityEditor.EditorApplication.delayCall += Validate;
#endif

        protected virtual void Validate()
        {
            if (this.IsNull())
                return;

            if (string.IsNullOrEmpty(data.colorGuid))
            {
                if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                    Debug.LogError($"[Theme] Color GUID is <b><color=red>null</color></b> at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
            if (!data.IsConnected)
            {
                if (Theme.IsLogActive(DebugLevel.Error) && this.IsNotNull())
                    Debug.LogError($"[Theme] Color with GUID='{data.colorGuid}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
#if UNITY_EDITOR
            InvalidateColor(Theme.Instance.CurrentTheme);
#endif
        }
    }
}