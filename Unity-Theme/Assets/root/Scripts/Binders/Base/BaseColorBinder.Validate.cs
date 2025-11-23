namespace Unity.Theme.Binders
{
    public abstract partial class BaseColorBinder : LogableMonoBehaviour
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
#if UNITY_EDITOR
                LogError("Color GUID is <b><color=red>null</color></b> or <b><color=red>empty</color></b>");
#else
                LogError("Color GUID is null or empty");
#endif
                return;
            }
            if (!data.IsConnected)
            {
                LogError("Color with GUID='{0}' not found in database", data.colorGuid);
                return;
            }
#if UNITY_EDITOR
            InvalidateColor(Theme.Instance.CurrentTheme);
#endif
        }
    }
}