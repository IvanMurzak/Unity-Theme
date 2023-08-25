using System;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class ThemeDatabase : ScriptableObject
    {
        public const string PATH                    = "Assets/Resources/Unity-Theme Database.asset";
        public const string PATH_FOR_RESOURCES_LOAD = "Unity-Theme Database";

        private static ThemeDatabase instance;
        public  static ThemeDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetOrCreateInstance();
                    if (instance == null)
                    {
                        Debug.LogError($"Can't find <b>Unity-Theme database file</b> at <i>{PATH}</i>");
                        return null;
                    }
                }
                return instance;
            }
        }

        private static ThemeDatabase GetOrCreateInstance()
        {
#if UNITY_EDITOR
            var config = UnityEditor.AssetDatabase.LoadAssetAtPath<ThemeDatabase>(PATH);
#else
            var config = Resources.Load<ThemeDatabase>(PATH_FOR_RESOURCES_LOAD);
#endif
            if (!config) throw new NullReferenceException("Unity-Theme database was not found");
            return config;
        }
    }

#pragma warning restore CA2235 // Mark all non-serializable fields
}