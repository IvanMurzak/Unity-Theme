using System;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ColorDataRef
    {
        [HideInInspector]                           
        public string   guid;
        public string   name = "New";

        public static int Compare(ColorDataRef l, ColorDataRef r)
        {
            return l.name.CompareTo(r.name);
        }
    }
}