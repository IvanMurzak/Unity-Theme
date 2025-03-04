using System;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ColorDataRef
    {
        [SerializeField, HideInInspector]
        private string guid;

        public string name = "New";

        public string Guid => guid;

        public ColorDataRef() { }
        public ColorDataRef(string guid, string name)
        {
            this.guid = guid;
            this.name = name;
        }

        public static int CompareByName(ColorDataRef l, ColorDataRef r)
            => l.name.CompareTo(r.name);

        public static int CompareByName(string colorNameL, string colorNameR)
            => colorNameL.CompareTo(colorNameR);
    }
}