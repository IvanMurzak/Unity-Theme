using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme : ScriptableObject
    {
        public const string ASSETS_PATH = "Assets/Resources/Unity-Theme Database.asset";
        public const string RESOURCES_PATH = "Unity-Theme Database";

#if UNITY_EDITOR
        public static Theme GetOrCreateInstance()
        {
            var config = Application.isPlaying
                ? Resources.Load<Theme>(RESOURCES_PATH)
                : UnityEditor.AssetDatabase.LoadAssetAtPath<Theme>(ASSETS_PATH);

            if (config == null)
            {
                Debug.Log($"<color=orange><b>Creating Unity-Theme database file</b> at <i>{ASSETS_PATH}</i></color>");
                config = ScriptableObject.CreateInstance<Theme>();

                config.SetDefaultPalettes();

                var directory = System.IO.Path.GetDirectoryName(ASSETS_PATH);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                UnityEditor.AssetDatabase.CreateAsset(config, ASSETS_PATH);
                UnityEditor.AssetDatabase.SaveAssets();
            }
            return config;
        }
#else
        public static Theme GetOrCreateInstance()
        {
            var config = Resources.Load<Theme>(RESOURCES_PATH);
            if (config == null)
            {
                Debug.LogError($"Can't find <b>Unity-Theme database file</b> at <i>{ASSETS_PATH}</i>");
            }
            return config;
        }
#endif
        public void SetDefaultPalettes()
        {
            SetOrAddTheme("Light", true);

            SetOrAddColor("Primary",                  "#6750A4");
            SetOrAddColor("On Primary",               "#FFFFFF");
            SetOrAddColor("Primary Container",        "#EADDFF");
            SetOrAddColor("On Primary Container",     "#21005D");
            
            SetOrAddColor("Secondary",                "#625B71");
            SetOrAddColor("On Secondary",             "#FFFFFF");
            SetOrAddColor("Secondary Container",      "#E8DEF8");
            SetOrAddColor("On Secondary Container",   "#1D192B");

            SetOrAddColor("Tertiary",                 "#7D5260");
            SetOrAddColor("On Tertiary",              "#FFFFFF");
            SetOrAddColor("Tertiary Container",       "#FFD8E4");
            SetOrAddColor("On Tertiary Container",    "#31111D");

            SetOrAddColor("Error",                    "#B3261E");
            SetOrAddColor("On Error",                 "#FFFFFF");
            SetOrAddColor("Error Container",          "#F9DEDC");
            SetOrAddColor("On Error Container",       "#410E0B");
            
            SetOrAddColor("Background",               "#FFFBFE");
            SetOrAddColor("On Background",            "#1C1B1F");
            SetOrAddColor("Surface",                  "#FFFBFE");
            SetOrAddColor("On Surface",               "#1C1B1F");
            
            SetOrAddColor("Outline",                  "#79747E");
            SetOrAddColor("Surface-Variant",          "#E7E0EC");
            SetOrAddColor("On Surface-Variant",       "#49454F");

            // ---------------------------------------------------------

            SetOrAddTheme("Dark", true);

            SetOrAddColor("Primary",                  "#D0BCFF");
            SetOrAddColor("On Primary",               "#381E72");
            SetOrAddColor("Primary Container",        "#4F378B");
            SetOrAddColor("On Primary Container",     "#EADDFF");
            
            SetOrAddColor("Secondary",                "#CCC2DC");
            SetOrAddColor("On Secondary",             "#332D41");
            SetOrAddColor("Secondary Container",      "#4A4458");
            SetOrAddColor("On Secondary Container",   "#E8DEF8");

            SetOrAddColor("Tertiary",                 "#EFB8C8");
            SetOrAddColor("On Tertiary",              "#492532");
            SetOrAddColor("Tertiary Container",       "#633B48");
            SetOrAddColor("On Tertiary Container",    "#FFD8E4");

            SetOrAddColor("Error",                    "#F2B8B5");
            SetOrAddColor("On Error",                 "#601410");
            SetOrAddColor("Error Container",          "#8C1D18");
            SetOrAddColor("On Error Container",       "#F9DEDC");
            
            SetOrAddColor("Background",               "#1C1B1F");
            SetOrAddColor("On Background",            "#E6E1E5");
            SetOrAddColor("Surface",                  "#1C1B1F");
            SetOrAddColor("On Surface",               "#E6E1E5");
            
            SetOrAddColor("Outline",                  "#938F99");
            SetOrAddColor("Surface-Variant",          "#49454F");
            SetOrAddColor("On Surface-Variant",       "#CAC4D0");

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

#pragma warning restore CA2235 // Mark all non-serializable fields
}