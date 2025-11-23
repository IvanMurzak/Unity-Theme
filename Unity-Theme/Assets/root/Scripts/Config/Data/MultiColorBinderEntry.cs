using System;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [Serializable]
    public class MultiColorBinderEntry
    {
        [SerializeField] public string label;
        [SerializeField] public ColorBinderData colorData;

        public MultiColorBinderEntry()
        {
            label = string.Empty;
            colorData = new ColorBinderData();
        }

        public MultiColorBinderEntry(string label)
        {
            this.label = label;
            colorData = new ColorBinderData();
        }

        public MultiColorBinderEntry(string label, ColorBinderData colorData)
        {
            this.label = label;
            this.colorData = colorData;
        }
    }
}
