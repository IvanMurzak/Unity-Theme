using System.Collections;
using UnityEditor;

namespace Unity.Theme.Editor
{
    public static class ThemeInitializer
    {
        [InitializeOnLoadMethod]
        public static IEnumerator Init()
        {
            yield return null; // let's Unity initialize itself and project resources first
            var config = Theme.Instance;
            ThemeWindowEditor.ShowWindow();
        }

        [MenuItem("Edit/Unity-Theme/Reset Default Palettes")]
        public static void ResetDefaultPalettes()
        {
            var config = Theme.Instance;
            
            config.RemoveAllThemes();
            config.RemoveAllColors();

            config.SetDefaultPalettes();

            ThemeWindowEditor.ShowWindow().Invalidate();
        }
        [MenuItem("Edit/Unity-Theme/Set Default Palettes")]
        public static void SetDefaultPalettes()
        {
            var config = Theme.Instance;
            config.SetDefaultPalettes();

            ThemeWindowEditor.ShowWindow().Invalidate();
        }
    }
}