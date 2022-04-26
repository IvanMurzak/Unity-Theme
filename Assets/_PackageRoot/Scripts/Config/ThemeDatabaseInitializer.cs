using System;
using UnityEngine;

namespace Unity.Theme
{
    public static class ThemeDatabaseInitializer
    {
        private static ThemeDatabase _config;
        public static ThemeDatabase Config => _config == null ? _config = LoadOrCreateConfig() : _config;

        private static ThemeDatabase LoadOrCreateConfig()
        {
#if UNITY_EDITOR
            var config = UnityEditor.AssetDatabase.LoadAssetAtPath<ThemeDatabase>(ThemeDatabase.PATH);
#else
            var config = Resources.Load<ThemeDatabase>(ThemeDatabase.PATH_FOR_RESOURCES_LOAD);
#endif
            if (!config) throw new NullReferenceException("Unity-Theme database was not found");
            return config;
        }
    }
}