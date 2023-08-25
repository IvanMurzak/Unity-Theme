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

        [MenuItem("Edit/Unity-Theme/Reset Default Palettes")]
        public static void ResetDefaultPalettes()
        {
            var config = GetOrCreateConfig();
            ResetDefaultPalettes(config);
        }

        public static Theme GetOrCreateConfig()
        {
            var config = Resources.Load<Theme>(Theme.PATH_FOR_RESOURCES_LOAD);
            if (config == null)
            {
                Debug.Log($"<color=orange><b>Creating Unity-Theme database file</b> at <i>{Theme.PATH}</i></color>");
                config = ScriptableObject.CreateInstance<Theme>();

                ResetDefaultPalettes(config);

                var directory = Path.GetDirectoryName(Theme.PATH);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                AssetDatabase.CreateAsset(config, Theme.PATH);
                AssetDatabase.SaveAssets();
            }
            return config;
        }

        public static void ResetDefaultPalettes(Theme config)
        {
            config.RemoveAllThemes();            
            config.RemoveAllColors();

            config.AddTheme("Light");

            config.AddColor("Primary",                  "#6750A4");
            config.AddColor("On Primary",               "#FFFFFF");
            config.AddColor("Primary Container",        "#EADDFF");
            config.AddColor("On Primary Container",     "#21005D");
            
            config.AddColor("Secondary",                "#625B71");
            config.AddColor("On Secondary",             "#FFFFFF");
            config.AddColor("Secondary Container",      "#E8DEF8");
            config.AddColor("On Secondary Container",   "#1D192B");

            config.AddColor("Tertiary",                 "#7D5260");
            config.AddColor("On Tertiary",              "#FFFFFF");
            config.AddColor("Tertiary Container",       "#FFD8E4");
            config.AddColor("On Tertiary Container",    "#31111D");

            config.AddColor("Error",                    "#B3261E");
            config.AddColor("On Error",                 "#FFFFFF");
            config.AddColor("Error Container",          "#F9DEDC");
            config.AddColor("On Error Container",       "#410E0B");
            
            config.AddColor("Background",               "#FFFBFE");
            config.AddColor("On Background",            "#1C1B1F");
            config.AddColor("Surface",                  "#FFFBFE");
            config.AddColor("On Surface",               "#1C1B1F");
            
            config.AddColor("Outline",                  "#79747E");
            config.AddColor("Surface-Variant",          "#E7E0EC");
            config.AddColor("On Surface-Variant",       "#49454F");

            // ----------------------------------------------------

            config.AddTheme("Dark", true);

            config.SetColor("Primary",                  "#D0BCFF");
            config.SetColor("On Primary",               "#381E72");
            config.SetColor("Primary Container",        "#4F378B");
            config.SetColor("On Primary Container",     "#EADDFF");
            
            config.SetColor("Secondary",                "#CCC2DC");
            config.SetColor("On Secondary",             "#332D41");
            config.SetColor("Secondary Container",      "#4A4458");
            config.SetColor("On Secondary Container",   "#E8DEF8");

            config.SetColor("Tertiary",                 "#EFB8C8");
            config.SetColor("On Tertiary",              "#492532");
            config.SetColor("Tertiary Container",       "#633B48");
            config.SetColor("On Tertiary Container",    "#FFD8E4");

            config.SetColor("Error",                    "#F2B8B5");
            config.SetColor("On Error",                 "#601410");
            config.SetColor("Error Container",          "#8C1D18");
            config.SetColor("On Error Container",       "#F9DEDC");
            
            config.SetColor("Background",               "#1C1B1F");
            config.SetColor("On Background",            "#E6E1E5");
            config.SetColor("Surface",                  "#1C1B1F");
            config.SetColor("On Surface",               "#E6E1E5");
            
            config.SetColor("Outline",                  "#938F99");
            config.SetColor("Surface-Variant",          "#49454F");
            config.SetColor("On Surface-Variant",       "#CAC4D0");
            
            EditorUtility.SetDirty(config);
        }
    }
}