using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Theme.Binders;
using System.Linq;
using NUnit.Framework;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        const string TestThemeName = "TestTheme";

        public static IEnumerator ColorBinder_SetColor_Image<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            SetColor(colorBinder, colorData);
            Assert.AreEqual(color, getter(target));
            yield return null;
        }

        public static IEnumerator ColorBinder_SetColor_OverrideAlpha<T, B>(Func<T, Color> getter, float alpha = 0.2f) where T : Component where B : GenericColorBinder<T>
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            SetColor(colorBinder, colorData);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(color.SetA(alpha), getter(target));

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(color, getter(target));
            yield return null;
        }

        public static IEnumerator ColorBinder_SwitchColor<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(0).First());
            var colorData2 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(1).First());

            var color1 = colorData1.Color;
            var color2 = colorData2.Color;

            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            SetColor(colorBinder, colorData1);
            Assert.AreEqual(color1, getter(target));

            SetColor(colorBinder, colorData2);
            Assert.AreEqual(color2, getter(target));
            yield return null;
        }
        public static IEnumerator ColorBinder_SwitchTheme<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(0).First());
            var colorData2 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(1).First());

            colorData1.Color = Color.cyan;

            var color1 = colorData1.Color;
            var color2 = colorData2.Color;

            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            TestUtils.SetColor(colorBinder, colorData1);
            Assert.AreEqual(color1, getter(target));

            Theme.Instance.SetOrAddTheme(TestThemeName, setCurrent: true);

            TestUtils.SetColor(colorBinder, colorData2);
            Assert.AreEqual(color2, getter(target));
            yield return null;
        }
    }
}