using System;
using System.Linq;
using System.Collections.Generic;

namespace Unity.Theme
{
    [Serializable]
    public class ThemeData
    {
        public string themeName = "New Theme";
        public List<ColorData> colors = new List<ColorData>();

        public ColorData GetColorByGuid(string guid) => string.IsNullOrEmpty(guid) ? null : colors?.FirstOrDefault(x => x.guid == guid);
    }
}