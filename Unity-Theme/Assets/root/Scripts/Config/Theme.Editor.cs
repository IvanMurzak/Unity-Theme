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
        public void InvalidateAssetFile() => UnityEditor.AssetDatabase.ImportAsset(AssetsFilePath, UnityEditor.ImportAssetOptions.ForceUpdate);
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
                    Debug.LogError($"[Theme] <color=red><b>{ResourcesFileName}</b> file is corrupted at <i>{AssetsFilePath}</i></color>");
                    Debug.LogException(e);
                }
                if (config == null)
                {
                    Debug.Log($"[Theme] <color=orange><b>Creating {ResourcesFileName}</b> file at <i>{AssetsFilePath}</i></color>");
                    config = new Theme();
                    config.SetDefaultPalettes();
                }
                return config;
            }
            catch (Exception e)
            {
                Debug.LogError($"[Theme] <color=red><b>{ResourcesFileName}</b> file can't be loaded from <i>{AssetsFilePath}</i></color>");
                Debug.LogException(e);
            }
            return null;
        }

        public void SetDefaultPalettes()
        {
            SetOrAddTheme("Light", true);

            SetOrAddColor("Primary",                    "#6750A4");
            SetOrAddColor("Primary Text",               "#FFFFFF");
            SetOrAddColor("Primary Container",          "#EADDFF");
            SetOrAddColor("Primary Container Text",     "#21005D");

            SetOrAddColor("Secondary",                  "#625B71");
            SetOrAddColor("Secondary Text",             "#FFFFFF");
            SetOrAddColor("Secondary Container",        "#E8DEF8");
            SetOrAddColor("Secondary Container Text",   "#1D192B");

            SetOrAddColor("Tertiary",                   "#7D5260");
            SetOrAddColor("Tertiary Text",              "#FFFFFF");
            SetOrAddColor("Tertiary Container",         "#FFD8E4");
            SetOrAddColor("Tertiary Container Text",    "#31111D");

            SetOrAddColor("Error",                      "#B3261E");
            SetOrAddColor("Error Text",                 "#FFFFFF");
            SetOrAddColor("Error Container",            "#F9DEDC");
            SetOrAddColor("Error Container Text",       "#410E0B");

            SetOrAddColor("Background",                 "#FFFBFE");
            SetOrAddColor("Background Text",            "#1C1B1F");
            SetOrAddColor("Surface",                    "#FFFBFE");
            SetOrAddColor("Surface Text",               "#1C1B1F");

            SetOrAddColor("Outline",                    "#79747E");
            SetOrAddColor("Surface-Variant",            "#E7E0EC");
            SetOrAddColor("Surface-Variant Text",       "#49454F");

            // ---------------------------------------------------------

            SetOrAddTheme("Dark", true);

            SetOrAddColor("Primary",                    "#D0BCFF");
            SetOrAddColor("Primary Text",               "#381E72");
            SetOrAddColor("Primary Container",          "#4F378B");
            SetOrAddColor("Primary Container Text",     "#EADDFF");

            SetOrAddColor("Secondary",                  "#CCC2DC");
            SetOrAddColor("Secondary Text",             "#332D41");
            SetOrAddColor("Secondary Container",        "#4A4458");
            SetOrAddColor("Secondary Container Text",   "#E8DEF8");

            SetOrAddColor("Tertiary",                   "#EFB8C8");
            SetOrAddColor("Tertiary Text",              "#492532");
            SetOrAddColor("Tertiary Container",         "#633B48");
            SetOrAddColor("Tertiary Container Text",    "#FFD8E4");

            SetOrAddColor("Error",                      "#F2B8B5");
            SetOrAddColor("Error Text",                 "#601410");
            SetOrAddColor("Error Container",            "#8C1D18");
            SetOrAddColor("Error Container Text",       "#F9DEDC");

            SetOrAddColor("Background",                 "#1C1B1F");
            SetOrAddColor("Background Text",            "#E6E1E5");
            SetOrAddColor("Surface",                    "#1C1B1F");
            SetOrAddColor("Surface Text",               "#E6E1E5");

            SetOrAddColor("Outline",                    "#938F99");
            SetOrAddColor("Surface-Variant",            "#49454F");
            SetOrAddColor("Surface-Variant Text",       "#CAC4D0");

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

                var assetFile = AssetFile;
                if (assetFile != null)
                    UnityEditor.EditorUtility.SetDirty(assetFile);
                else
                    UnityEditor.AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError($"[Theme] <color=red><b>{ResourcesFileName}</b> file can't be saved at <i>{AssetsFilePath}</i></color>");
                Debug.LogException(e);
            }
#else
            return;
#endif
        }
    }

#pragma warning restore CA2235 // Mark all non-serializable fields
}