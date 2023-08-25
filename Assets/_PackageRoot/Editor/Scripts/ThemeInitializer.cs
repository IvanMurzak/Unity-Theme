using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unity.Theme.Editor
{
    public static class ThemeInitializer
    {
        [InitializeOnLoadMethod]
        public static IEnumerator Init()
        {
            yield return null; // let's Unity initialize itself and project resources first
            var config = GetOrCreateConfig();
            ThemeWindowEditor.ShowWindow();
        }

        public static Theme GetOrCreateConfig()
        {
            var config = Resources.Load<Theme>(Theme.PATH_FOR_RESOURCES_LOAD);
            if (config == null)
            {
                Debug.Log($"<color=orange><b>Creating Unity-Theme database file</b> at <i>{Theme.PATH}</i></color>");
                config = ScriptableObject.CreateInstance<Theme>();

                config.AddTheme("Default");

                config.AddColor("Primary",          "#B0BEC5FF");
                config.AddColor("Primary Dark",     "#809492FF");
                config.AddColor("Primary Light",    "#E1F7F2FF");

                config.AddColor("Secondary",        "#212121FF");
                config.AddColor("Secondary Light",  "#000000FF");
                config.AddColor("Secondary Dark",   "#484848FF");

                config.AddColor("Accent",           "#FA6960FF");
                config.AddColor("On Accent",        "#FFFFFFFF");
                config.AddColor("On Primary",       "#000000FF");
                config.AddColor("On Secondary",     "#FFFFFFFF");

                string directory = Path.GetDirectoryName(Theme.PATH);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                AssetDatabase.CreateAsset(config, Theme.PATH);
                AssetDatabase.SaveAssets();
            }
            return config;
        }
    }
}