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

            Theme.Instance.CurrentThemeName = Theme1Name;

            SetColorByName(colorBinder, Color1Name);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));
        }

        public static IEnumerator ColorBinder_SetColor_OverrideAlpha<T, B>(Func<T, Color> getter, float alpha = 0.2f) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = Theme1Name;

            SetColorByName(colorBinder, Color1Name);
            SetAlphaOverride(colorBinder, overrideAlpha: true, alpha);
            Assert.AreEqual(Color1Theme1Value.HexToColor().SetA(alpha), getter(target));

            SetAlphaOverride(colorBinder, overrideAlpha: false, alpha);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));
        }

        public static IEnumerator ColorBinder_SwitchColor<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = Theme1Name;

            SetColorByName(colorBinder, Color1Name);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));

            SetColorByName(colorBinder, Color2Name);
            Assert.AreEqual(Color2Theme1Value.HexToColor(), getter(target));
        }
        public static IEnumerator ColorBinder_SwitchTheme<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = Theme1Name;

            SetColorByName(colorBinder, Color1Name);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));

            Theme.Instance.CurrentThemeName = Theme2Name;
            Assert.AreEqual(Color1Theme2Value.HexToColor(), getter(target));

            SetColorByName(colorBinder, Color2Name);
            Assert.AreEqual(Color2Theme2Value.HexToColor(), getter(target));
        }
    }
}