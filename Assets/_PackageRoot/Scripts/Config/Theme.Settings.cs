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
    }
    public enum DebugLevel
    {
        Trace       = -1,
        Log         = 0,
        Warning     = 1,
        Error       = 2,
        Exception   = 3,
        None        = 4
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}