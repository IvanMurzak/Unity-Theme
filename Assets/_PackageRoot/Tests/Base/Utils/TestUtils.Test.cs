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
            var colorData1 = Theme.Instance.GetColorByName(Color1Name);
            var color = colorData1.Color;
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            SetColor(colorBinder, colorData1);
            Assert.AreEqual(color, getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
            yield return null;
        }

        public static IEnumerator ColorBinder_SetColor_OverrideAlpha<T, B>(Func<T, Color> getter, float alpha = 0.2f) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByName(Color1Name);
            var color = colorData1.Color;
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            SetColor(colorBinder, colorData1);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(color.SetA(alpha), getter(target));

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(color, getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
            yield return null;
        }

        public static IEnumerator ColorBinder_SwitchColor<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByName(Color1Name);
            var colorData2 = Theme.Instance.GetColorByName(Color2Name);

            var color1 = colorData1.Color;
            var color2 = colorData2.Color;

            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            SetColor(colorBinder, colorData1);
            Assert.AreEqual(color1, getter(target));

            SetColor(colorBinder, colorData2);
            Assert.AreEqual(color2, getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
            yield return null;
        }
        public static IEnumerator ColorBinder_SwitchTheme<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByName(Color1Name);
            var colorData2 = Theme.Instance.GetColorByName(Color2Name);

            colorData1.Color = Color.cyan;

            var color1 = colorData1.Color;
            var color2 = colorData2.Color;

            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            TestUtils.SetColor(colorBinder, colorData1);
            Assert.AreEqual(color1, getter(target));

            Theme.Instance.SetOrAddTheme(Theme1Name, setCurrent: true);

            TestUtils.SetColor(colorBinder, colorData2);
            Assert.AreEqual(color2, getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
            yield return null;
        }
    }
}