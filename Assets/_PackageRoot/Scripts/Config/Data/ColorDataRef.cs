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

        public ColorDataRef()
        {
            guid = System.Guid.NewGuid().ToString();
        }
        public ColorDataRef(string guid, string name)
        {
            this.guid = guid;
            this.name = name;
        }

        public static int Compare(ColorDataRef l, ColorDataRef r)
            => l.name.CompareTo(r.name);

        public static int Compare(string colorNameL, string colorNameR)
            => colorNameL.CompareTo(colorNameR);
    }
}