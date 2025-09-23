using System;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ColorData
    {
        [SerializeField, HideInInspector]
        private string guid;

        [SerializeField, HideInInspector]
        private string colorHex = "#FFFFFFFF";

        public Color Color
        {
            get => ColorUtility.TryParseHtmlString(colorHex, out var color)
                ? color
                : Color.white;
            internal set => colorHex = value.ToHexRGBA();
        }

        public string Guid => guid;

        public ColorData() { }
        public ColorData(ColorData colorData) : this(colorData.guid, colorData.colorHex) { }
        public ColorData(ColorDataRef colorRef) : this(colorRef, Color.white) { }
        public ColorData(ColorDataRef colorRef, Color color) : this(colorRef.Guid, color) { }
        public ColorData(string guid, Color color) : this(guid, color.ToHexRGBA()) { }
        public ColorData(string guid, string colorHex) : this()
        {
            this.guid = guid;
            this.colorHex = colorHex;
        }
    }
}