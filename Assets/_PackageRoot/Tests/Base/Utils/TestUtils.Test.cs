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
        public static IEnumerator ColorBinder_SwitchColor<T, B>(Func<T, Color> getter) where T : Component where B : GenericColorBinder<T>
        {
            var colorData1 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(0).First());
            var colorData2 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(1).First());

            var color1 = colorData1.Color;
            var color2 = colorData2.Color;

            var colorBinder = CreateGenericColorBinder<T, B>(out var target);
            yield return null;

            TestUtils.SetColor(colorBinder, colorData1);
            Assert.AreEqual(color1, getter(target));

            TestUtils.SetColor(colorBinder, colorData2);
            Assert.AreEqual(color2, getter(target));
            yield return null;
        }
    }
}