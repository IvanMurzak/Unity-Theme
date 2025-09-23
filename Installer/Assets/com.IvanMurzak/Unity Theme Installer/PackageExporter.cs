using System.IO;
using UnityEditor;
using UnityEngine;

namespace com.IvanMurzak.Unity.Theme.Installer
{
    public static class PackageExporter
    {
        [MenuItem("Tools/Export Installer Package")]
        public static void ExportPackage()
        {
            var packagePath = "Assets/com.IvanMurzak/Unity Theme Installer";
            var outputPath = "build/Unity-Theme-Installer.unitypackage";

            // Ensure build directory exists
            var buildDir = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(buildDir))
            {
                Directory.CreateDirectory(buildDir);
            }

            // Export the package
            AssetDatabase.ExportPackage(packagePath, outputPath, ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);

            Debug.Log($"Package exported to: {outputPath}");
        }
    }
}