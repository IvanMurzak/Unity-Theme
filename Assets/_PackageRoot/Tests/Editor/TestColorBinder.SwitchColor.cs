using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using System.Linq;

namespace Unity.Theme.Tests.Editor
{
    public partial class TestColorBinder : TestBase
    {
        [UnityTest] public IEnumerator ColorBinder_SwitchColor_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchColor_Image);
        [UnityTest] public IEnumerator ColorBinder_SwitchColor_Image()
        {
            var colorData1 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(0).First());
            var colorData2 = Theme.Instance.GetColorByGuid(Theme.Instance.ColorGuids.Skip(1).First());

            var color1 = colorData1.Color;
            var color2 = colorData2.Color;

            var colorBinder = TestUtils.CreateGenericColorBinder<Image, ImageColorBinder>(out var target);
            yield return null;

            TestUtils.SetColor(colorBinder, colorData1);
            Assert.AreEqual(color1, target.color);

            TestUtils.SetColor(colorBinder, colorData2);
            Assert.AreEqual(color2, target.color);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SwitchColor_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchColor_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_SwitchColor_TextMeshProUGUI()
        {
            TestUtils.CreateGenericColorBinder<TextMeshProUGUI, TextMeshProColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SwitchColor_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchColor_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_SwitchColor_SpriteRenderer()
        {
            TestUtils.CreateGenericColorBinder<SpriteRenderer, SpriteRendererColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SwitchColor_Shadow_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchColor_Shadow);
        [UnityTest] public IEnumerator ColorBinder_SwitchColor_Shadow()
        {
            TestUtils.CreateGenericColorBinder<Shadow, ShadowColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_SwitchColor_Outline_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchColor_Outline);
        [UnityTest] public IEnumerator ColorBinder_SwitchColor_Outline()
        {
            TestUtils.CreateGenericColorBinder<Outline, OutlineColorBinder>(out var target);
            yield return null;
        }
    }
}