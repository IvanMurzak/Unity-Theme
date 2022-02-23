using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ColorName
    {
        public string name = "New";
        public string guid;

        public static int Compare(ColorData l, ColorData r)
        {
            return l.name.CompareTo(r.name);
        }
    }
}