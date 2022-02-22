using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unity.Theme
{
    public static class ThemeDatabaseConfigMenu
    {
        [InitializeOnLoadMethod]
        public static IEnumerator Init()
        {
            yield return null; // let's Unity initialize itself and project resources first
            GetOrCreateConfig();
        }

        [MenuItem("Edit/Unity-Theme Database", false, 250)]
        public static void OpenOrCreateConfig()
        {
            var config = GetOrCreateConfig();

            EditorUtility.FocusProjectWindow();
            EditorWindow inspectorWindow = EditorWindow.GetWindow(typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow"));
            inspectorWindow.Focus();

            Selection.activeObject = config;
        }

        public static ThemeDatabase GetOrCreateConfig()
        {
            var config = Resources.Load<ThemeDatabase>(ThemeDatabase.PATH_FOR_RESOURCES_LOAD);
            if (config == null)
            {
                Debug.Log($"<color=orange><b>Creating Unity-Theme database file</b> at <i>{ThemeDatabase.PATH}</i></color>");
                config = ScriptableObject.CreateInstance<ThemeDatabase>();

                string directory = Path.GetDirectoryName(ThemeDatabase.PATH);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                AssetDatabase.CreateAsset(config, ThemeDatabase.PATH);
                AssetDatabase.SaveAssets();
            }
            return config;
        }
    }
}