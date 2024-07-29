using UnityEngine.UIElements;

namespace Unity.Theme.Editor
{
    public static class UIElementsExtensions
    {
        public static void SubscribeOnValueChanged<T1>(this INotifyValueChanged<T1> target, TemplateContainer root, EventCallback<ChangeEvent<T1>> callback)
        {
            target.RegisterValueChangedCallback(callback);
            root.RegisterCallback<DetachFromPanelEvent>(_ =>
            {
                target.UnregisterValueChangedCallback(callback);
            });
        }
    }
}