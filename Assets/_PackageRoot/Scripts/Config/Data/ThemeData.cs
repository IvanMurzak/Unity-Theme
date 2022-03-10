using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Unity.Theme
{
    [Serializable]
    public class ThemeData
    {
        [TableColumnWidth(1), HideLabel]
        public string themeName = "New Theme";

        [HideReferenceObjectPicker, HideLabel]
        [TableList(AlwaysExpanded = true, NumberOfItemsPerPage = 20, IsReadOnly = true, HideToolbar = true)]
        public List<ColorData> colors = new List<ColorData>();

        public ColorData GetColorByGuid(string guid) => string.IsNullOrEmpty(guid) ? null : colors?.FirstOrDefault(x => x.guid == guid);

        [GUIColor(1, 0.2f, 0.2f, 1)]
        [Button, LabelText("REMOVE")] void Remove() => ThemeDatabaseInitializer.Config.RemoveTheme(this);
    }
}