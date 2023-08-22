using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Unity.Theme.Editor.UI
{
    public class ControlPanelEditor : EditorWindow
    {
        [MenuItem("Window/Unity-Theme")]
        public static void ShowExample()
        {
            var wnd = GetWindow<ControlPanelEditor>();
            wnd.titleContent = new GUIContent("ControlPanelEditor");
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;

            root.Add(new Label("Unity Theme"));

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_PackageRoot/Editor/UI/UnityTheme-ControlPanel.uxml");
            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_PackageRoot/Editor/UI/UnityTheme-ControlPanel.uss");
            VisualElement labelWithStyle = new Label("Hello World! With Style");
            labelWithStyle.styleSheets.Add(styleSheet);
            root.Add(labelWithStyle);
        }
    }
}