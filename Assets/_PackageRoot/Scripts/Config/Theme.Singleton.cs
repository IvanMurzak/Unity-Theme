using UnityEngine;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme : ScriptableObject
    {
        private static Theme instance;
        public  static Theme Instance
        {
            get
            {
                instance ??= GetOrCreateInstance();

                if (instance == null)
                    Debug.LogError("Theme instance is null");

                return instance;
            }
        }
    }

#pragma warning restore CA2235 // Mark all non-serializable fields
}