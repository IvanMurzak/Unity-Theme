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
            
            config.RemoveAllThemes();
            config.RemoveAllColors();

            SetDefaultPalettes(config);

            ThemeWindowEditor.ShowWindow().Invalidate();
        }
        [MenuItem("Edit/Unity-Theme/Set Default Palettes")]
        public static void SetDefaultPalettes()
        {
            var config = GetOrCreateConfig();
            SetDefaultPalettes(config);

            ThemeWindowEditor.ShowWindow().Invalidate();
        }

        public static Theme GetOrCreateConfig()
        {
            var config = Resources.Load<Theme>(Theme.PATH_FOR_RESOURCES_LOAD);
            if (config == null)
            {
                Debug.Log($"<color=orange><b>Creating Unity-Theme database file</b> at <i>{Theme.PATH}</i></color>");
                config = ScriptableObject.CreateInstance<Theme>();

                SetDefaultPalettes(config);

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

        public static void SetDefaultPalettes(Theme config)
        {
            config.SetOrAddTheme("Light", true);

            config.SetOrAddColor("Primary",                  "#6750A4");
            config.SetOrAddColor("On Primary",               "#FFFFFF");
            config.SetOrAddColor("Primary Container",        "#EADDFF");
            config.SetOrAddColor("On Primary Container",     "#21005D");
            
            config.SetOrAddColor("Secondary",                "#625B71");
            config.SetOrAddColor("On Secondary",             "#FFFFFF");
            config.SetOrAddColor("Secondary Container",      "#E8DEF8");
            config.SetOrAddColor("On Secondary Container",   "#1D192B");

            config.SetOrAddColor("Tertiary",                 "#7D5260");
            config.SetOrAddColor("On Tertiary",              "#FFFFFF");
            config.SetOrAddColor("Tertiary Container",       "#FFD8E4");
            config.SetOrAddColor("On Tertiary Container",    "#31111D");

            config.SetOrAddColor("Error",                    "#B3261E");
            config.SetOrAddColor("On Error",                 "#FFFFFF");
            config.SetOrAddColor("Error Container",          "#F9DEDC");
            config.SetOrAddColor("On Error Container",       "#410E0B");
            
            config.SetOrAddColor("Background",               "#FFFBFE");
            config.SetOrAddColor("On Background",            "#1C1B1F");
            config.SetOrAddColor("Surface",                  "#FFFBFE");
            config.SetOrAddColor("On Surface",               "#1C1B1F");
            
            config.SetOrAddColor("Outline",                  "#79747E");
            config.SetOrAddColor("Surface-Variant",          "#E7E0EC");
            config.SetOrAddColor("On Surface-Variant",       "#49454F");

            // ---------------------------------------------------------

            config.SetOrAddTheme("Dark", true);

            config.SetOrAddColor("Primary",                  "#D0BCFF");
            config.SetOrAddColor("On Primary",               "#381E72");
            config.SetOrAddColor("Primary Container",        "#4F378B");
            config.SetOrAddColor("On Primary Container",     "#EADDFF");
            
            config.SetOrAddColor("Secondary",                "#CCC2DC");
            config.SetOrAddColor("On Secondary",             "#332D41");
            config.SetOrAddColor("Secondary Container",      "#4A4458");
            config.SetOrAddColor("On Secondary Container",   "#E8DEF8");

            config.SetOrAddColor("Tertiary",                 "#EFB8C8");
            config.SetOrAddColor("On Tertiary",              "#492532");
            config.SetOrAddColor("Tertiary Container",       "#633B48");
            config.SetOrAddColor("On Tertiary Container",    "#FFD8E4");

            config.SetOrAddColor("Error",                    "#F2B8B5");
            config.SetOrAddColor("On Error",                 "#601410");
            config.SetOrAddColor("Error Container",          "#8C1D18");
            config.SetOrAddColor("On Error Container",       "#F9DEDC");
            
            config.SetOrAddColor("Background",               "#1C1B1F");
            config.SetOrAddColor("On Background",            "#E6E1E5");
            config.SetOrAddColor("Surface",                  "#1C1B1F");
            config.SetOrAddColor("On Surface",               "#E6E1E5");
            
            config.SetOrAddColor("Outline",                  "#938F99");
            config.SetOrAddColor("Surface-Variant",          "#49454F");
            config.SetOrAddColor("On Surface-Variant",       "#CAC4D0");
            
            EditorUtility.SetDirty(config);
        }
    }
}