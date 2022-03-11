using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Unity.Theme
{
    [Serializable]
    public class ThemeData
    {
        [TableColumnWidth(1), HideLabel, InlineButton("Remove", "REMOVE")]
        public string themeName = "New Theme";

        [GUIColor(1, 0.2f, 0.2f, 1)]
        void Remove() => ThemeDatabaseInitializer.Config.RemoveTheme(this);

        [HideReferenceObjectPicker, HideLabel]
        [TableList(AlwaysExpanded = true, NumberOfItemsPerPage = 20, IsReadOnly = true, HideToolbar = true)]
        public List<ColorData> colors = new List<ColorData>();

        public ColorData GetColorByGuid(string guid) => string.IsNullOrEmpty(guid) ? null : colors?.FirstOrDefault(x => x.guid == guid);
    }
}