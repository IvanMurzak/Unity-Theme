using System.Collections;
using System;
using UnityEngine;
using Unity.Theme.Binders;
using NUnit.Framework;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        public static IEnumerator ColorBinder_SetColor_Image<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            SetColorByName(colorBinder, C_Color.Name1);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), getter(target));
        }

        public static IEnumerator ColorBinder_SetColor_OverrideAlpha<T, B>(Func<T, Color> getter, float alpha = 0.2f) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            SetColorByName(colorBinder, C_Color.Name1);
            SetAlphaOverride(colorBinder, overrideAlpha: true, alpha);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor().SetA(alpha), getter(target));

            SetAlphaOverride(colorBinder, overrideAlpha: false, alpha);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), getter(target));
        }

        public static IEnumerator ColorBinder_SwitchColor<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            SetColorByName(colorBinder, C_Color.Name1);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), getter(target));

            SetColorByName(colorBinder, C_Color.Name2);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), getter(target));
        }
        public static IEnumerator ColorBinder_UpdateColor<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            SetColorByName(colorBinder, C_Color.Name1);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), getter(target));

            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.ValueAlternative);
            yield return null;
            Assert.AreEqual(C_Theme1.Color1.ValueAlternative.HexToColor(), getter(target));

            SetColorByName(colorBinder, C_Color.Name2);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), getter(target));

            Theme.Instance.SetColor(C_Color.Name2, C_Theme1.Color2.ValueAlternative);
            yield return null;
            Assert.AreEqual(C_Theme1.Color2.ValueAlternative.HexToColor(), getter(target));

            SetColorByName(colorBinder, C_Color.Name1);
            Assert.AreEqual(C_Theme1.Color1.ValueAlternative.HexToColor(), getter(target));

            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.Value);
            yield return null;
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), getter(target));

        }
        public static IEnumerator ColorBinder_SwitchTheme<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            SetColorByName(colorBinder, C_Color.Name1);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), getter(target));

            Theme.Instance.CurrentThemeName = C_Theme2.Name;
            Assert.AreEqual(C_Theme2.Color1.Value.HexToColor(), getter(target));

            SetColorByName(colorBinder, C_Color.Name2);
            Assert.AreEqual(C_Theme2.Color2.Value.HexToColor(), getter(target));
        }
    }
}