using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ColorData
    {
        [TableColumnWidth(150, false), ReadOnly]
        public string   name = "New";
        [TableColumnWidth(50, true)]
        public Color    color = Color.white;

        [TableColumnWidth(25, false)]
        [HideLabel]
        [Button, LabelText("X")] void X() => ThemeDatabaseInitializer.Config.RemoveColor(this);

        public static int Compare(ColorData l, ColorData r)
        {
            return l.name.CompareTo(r.name);
        }
    }
}