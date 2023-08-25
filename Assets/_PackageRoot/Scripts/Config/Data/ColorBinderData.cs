using System;
using UnityEngine;

namespace Unity.Theme.Binders
{
    [Serializable]
    public class ColorBinderData
    {
        [SerializeField, HideInInspector] public string colorGuid;                             
        [SerializeField, HideInInspector] public bool overrideAlpha;
        [SerializeField, HideInInspector] public float alpha = 1f;

        public bool IsConnected => ThemeDatabase.Instance?.GetColorByGuid(colorGuid) != null;
        public string ColorName => ThemeDatabase.Instance?.GetColorName(colorGuid);
        public void ResetColor() => colorGuid = null;
    }
}