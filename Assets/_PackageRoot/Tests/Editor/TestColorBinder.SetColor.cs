using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests.Editor
{
    public partial class TestColorBinder : TestBase
    {
        [UnityTest] public IEnumerator ColorBinder_SetColor_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_Image);
        [UnityTest] public IEnumerator ColorBinder_SetColor_Image()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<Image, ImageColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);
            Assert.AreEqual(color, target.color);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_SetColor_TextMeshProUGUI()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<TextMeshProUGUI, TextMeshProColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);
            Assert.AreEqual(color, target.color);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_SetColor_SpriteRenderer()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<SpriteRenderer, SpriteRendererColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);
            Assert.AreEqual(color, target.color);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_Shadow_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_Shadow);
        [UnityTest] public IEnumerator ColorBinder_SetColor_Shadow()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<Shadow, ShadowColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);
            Assert.AreEqual(color, target.effectColor);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_Outline_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_Outline);
        [UnityTest] public IEnumerator ColorBinder_SetColor_Outline()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<Outline, OutlineColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);
            Assert.AreEqual(color, target.effectColor);
            yield return null;
        }
    }
}