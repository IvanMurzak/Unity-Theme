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
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.SetOrAddTheme(Theme1Name, setCurrent: true);

            SetColor(colorBinder, colorData1);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
        }

        public static IEnumerator ColorBinder_SetColor_OverrideAlpha<T, B>(Func<T, Color> getter, float alpha = 0.2f) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByName(Color1Name);
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.SetOrAddTheme(Theme1Name, setCurrent: true);

            SetColor(colorBinder, colorData1);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(Color1Theme1Value.HexToColor().SetA(alpha), getter(target));

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
        }

        public static IEnumerator ColorBinder_SwitchColor<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByName(Color1Name);
            var colorData2 = Theme.Instance.GetColorByName(Color2Name);
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.SetOrAddTheme(Theme1Name, setCurrent: true);

            SetColor(colorBinder, colorData1);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));

            SetColor(colorBinder, colorData2);
            Assert.AreEqual(Color2Theme1Value.HexToColor(), getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
        }
        public static IEnumerator ColorBinder_SwitchTheme<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByName(Color1Name);
            var colorData2 = Theme.Instance.GetColorByName(Color2Name);
            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.SetOrAddTheme(Theme1Name, setCurrent: true);

            SetColor(colorBinder, colorData1);
            Assert.AreEqual(Color1Theme1Value.HexToColor(), getter(target));

            SetColor(colorBinder, colorData2);
            Assert.AreEqual(Color2Theme1Value.HexToColor(), getter(target));

            UnityEngine.Object.DestroyImmediate(colorBinder.gameObject);
        }
    }
}