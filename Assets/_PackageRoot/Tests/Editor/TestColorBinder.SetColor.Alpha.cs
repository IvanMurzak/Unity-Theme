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
        const float alpha = 0.2f;

        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_OverrideAlpha_Image);
        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_Image()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<Image, ImageColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(color.SetA(alpha), target.color);

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(color, target.color);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_OverrideAlpha_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_TextMeshProUGUI()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<TextMeshProUGUI, TextMeshProColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(color.SetA(alpha), target.color);

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(color, target.color);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_OverrideAlpha_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_SpriteRenderer()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<SpriteRenderer, SpriteRendererColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(color.SetA(alpha), target.color);

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(color, target.color);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_Shadow_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_OverrideAlpha_Shadow);
        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_Shadow()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<Shadow, ShadowColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(color.SetA(alpha), target.effectColor);

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(color, target.effectColor);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_Outline_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_OverrideAlpha_Outline);
        [UnityTest] public IEnumerator ColorBinder_SetColor_OverrideAlpha_Outline()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<Outline, OutlineColorBinder>(out var target);
            yield return null;
            TestUtils.SetColor(colorBinder, colorData);

            colorBinder.SetAlpha(overrideAlpha: true, alpha);
            Assert.AreEqual(color.SetA(alpha), target.effectColor);

            colorBinder.SetAlpha(overrideAlpha: false);
            Assert.AreEqual(color, target.effectColor);
            yield return null;
        }
    }
}