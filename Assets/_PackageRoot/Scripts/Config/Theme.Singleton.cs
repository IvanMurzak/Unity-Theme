using System;
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
                if (instance == null)
                    instance = GetOrCreateInstance();

                return instance;
            }
        }
    }

#pragma warning restore CA2235 // Mark all non-serializable fields
}