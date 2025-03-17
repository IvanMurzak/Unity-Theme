using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Theme
{
    [Serializable]
    public class ThemeData
    {
        [SerializeField, HideInInspector]
        private string guid;

        public bool expanded = true;
        public string themeName = "New Theme";
        public List<ColorData> colors = new List<ColorData>();

        public ThemeData() { }
        public ThemeData(string guid) : this()
        {
            this.guid = guid;
        }
        public string Guid => guid;
        public ColorData GetColorByGuid(string guid) => string.IsNullOrEmpty(guid) ? null : colors?.FirstOrDefault(x => x.Guid == guid);
        public ColorData GetColorByRef(ColorDataRef colorRef) => GetColorByGuid(colorRef.Guid);
    }
}