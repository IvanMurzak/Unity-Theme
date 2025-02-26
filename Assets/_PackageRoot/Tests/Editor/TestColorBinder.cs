using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests.Editor
{
    public class TestColorBinder : TestBase
    {
        [SetUp] public override void SetUp() => base.SetUp();
        [TearDown] public override void TearDown() => base.TearDown();

        [UnityTest] public IEnumerator ColorBinder_Create_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_Image);
        [UnityTest] public IEnumerator ColorBinder_Create_Image()
        {
            TestUtils.CreateGenericColorBinder<Image, ImageColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_Create_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_Create_TextMeshProUGUI()
        {
            TestUtils.CreateGenericColorBinder<TextMeshProUGUI, TextMeshProColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_Create_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_Create_SpriteRenderer()
        {
            TestUtils.CreateGenericColorBinder<SpriteRenderer, SpriteRendererColorBinder>(out var target);
            yield return null;
        }

        // -------------------------------------------------------------------------------

        [UnityTest] public IEnumerator ColorBinder_SetColor_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_Image);
        [UnityTest] public IEnumerator ColorBinder_SetColor_Image()
        {
            var colorData = Theme.Instance.GetColorFirst();
            var color = colorData.Color;
            var colorBinder = TestUtils.CreateGenericColorBinder<Image, ImageColorBinder>(out var target);
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
            TestUtils.SetColor(colorBinder, colorData);
            Assert.AreEqual(color, target.color);
            yield return null;
        }
    }
}