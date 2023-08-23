using System;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ColorData
    {
        [HideInInspector]
        public string   guid;
        public string   name = "New";
        public Color    color = Color.white;

        public static int Compare(ColorData l, ColorData r)
        {
            return l.name.CompareTo(r.name);
        }
    }
}