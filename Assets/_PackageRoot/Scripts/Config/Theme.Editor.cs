using System;
using System.IO;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme
    {
        public static string ResourcesFileName => "Unity-Theme-Database";
        public static string AssetsFilePath => $"Assets/Resources/{ResourcesFileName}.json";
#if UNITY_EDITOR
        public TextAsset AssetFile => UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(AssetsFilePath);
#endif

        public static Theme GetOrCreateInstance()
        {
            try
            {
#if UNITY_EDITOR
                var json = Application.isPlaying
                    ? Resources.Load<TextAsset>(ResourcesFileName).text
                    : File.Exists(AssetsFilePath)
                        ? File.ReadAllText(AssetsFilePath)
                        : null;
#else
                var json = Resources.Load<TextAsset>(ResourcesFileName).text;
#endif
                Theme config = null;
                try { config = JsonUtility.FromJson<Theme>(json); }
                catch (Exception e)
                {
                    Debug.LogError($"<color=red><b>{ResourcesFileName}</b> file is corrupted at <i>{AssetsFilePath}</i></color>");
                    Debug.LogException(e);
                }
                if (config == null)
                {
                    Debug.Log($"<color=orange><b>Creating {ResourcesFileName}</b> file at <i>{AssetsFilePath}</i></color>");
                    config = new Theme();
                    config.SetDefaultPalettes();
                }
                return config;
            }
            catch (Exception e)
            {
                Debug.LogError($"<color=red><b>{ResourcesFileName}</b> file can't be loaded from <i>{AssetsFilePath}</i></color>");
                Debug.LogException(e);
            }
            return null;
        }

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

            Save();
        }

        public void Save()
        {
#if UNITY_EDITOR
            OnValidate();
            try
            {
                var directory = Path.GetDirectoryName(AssetsFilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var json = JsonUtility.ToJson(this, true);
                File.WriteAllText(AssetsFilePath, json);
                UnityEditor.EditorUtility.SetDirty(AssetFile);
            }
            catch (Exception e)
            {
                Debug.LogError($"<color=red><b>{ResourcesFileName}</b> file can't be saved at <i>{AssetsFilePath}</i></color>");
                Debug.LogException(e);
            }
#else
            return;
#endif
        }
    }

#pragma warning restore CA2235 // Mark all non-serializable fields
}