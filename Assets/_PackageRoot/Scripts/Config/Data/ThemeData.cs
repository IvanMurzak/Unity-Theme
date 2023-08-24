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

        public string themeName = "New Theme";
        public List<ColorData> colors = new List<ColorData>();
        
        public ThemeData()
        {
            guid = System.Guid.NewGuid().ToString();
        }
        public string Guid => guid;
        public ColorData GetColorByGuid(string guid) => string.IsNullOrEmpty(guid) ? null : colors?.FirstOrDefault(x => x.Guid == guid);
    }
}