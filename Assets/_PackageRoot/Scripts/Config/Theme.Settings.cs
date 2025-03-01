using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme
    {
        [SerializeField] public DebugLevel debugLevel = DebugLevel.Error;

        [HideInInspector]
        [SerializeField] int currentThemeIndex = 0;
        [SerializeField] List<ColorDataRef> colors = new();
        [SerializeField] List<ThemeData> themes = new();

        public static bool IsLogActive(DebugLevel logLevel)
        {
            var instance = Instance;
            if (instance == null)
                return false;
            return instance.debugLevel.IsActive(logLevel);
        }
    }
    public enum DebugLevel
    {
        Trace       = -1, // show all messages
        Log         = 0,  // show only Log, Warning, Error, Exception messages
        Warning     = 1,  // show only Warning, Error, Exception messages
        Error       = 2,  // show only Error, Exception messages
        Exception   = 3,  // show only Exception messages
        None        = 4   // show no messages
    }
    public static class DebugLevelEx
    {
        /// <summary>
        /// Check if the DebugLevel is active
        /// If it is active the related message will be shown in the console
        /// </summary>
        public static bool IsActive(this DebugLevel debugLevel, DebugLevel level) => debugLevel <= level;
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}