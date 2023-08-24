using System;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ColorData
    {
        [SerializeField, HideInInspector]
        private string guid;

        public Color color = Color.white;

        public string Guid => guid;

        public ColorData() { }
        public ColorData(ColorData colorData) : this(colorData.guid, colorData.color) { }
        public ColorData(ColorDataRef colorRef) : this(colorRef, Color.white) { }
        public ColorData(ColorDataRef colorRef, Color color) : this(colorRef.Guid, color) { }
        public ColorData(string guid, Color color) : this()
        {
            this.guid = guid;
            this.color = color;
        }
    }
}