using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator UpdateColors_Button_NoLogs() => TestUtils.RunNoLogs(UpdateColors_Button);
        [UnityTest] public IEnumerator UpdateColors_Button() =>
            TestUtils.MultiColorBinder_UpdateColor<Button, ButtonColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator UpdateColors_MultipleEntries_Button_NoLogs() => TestUtils.RunNoLogs(UpdateColors_MultipleEntries_Button);
        [UnityTest] public IEnumerator UpdateColors_MultipleEntries_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set initial colors
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByName(colorBinder, 1, TestUtils.C_Color.Name2);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor(), colorBlock.highlightedColor);

            // Update Color1 in theme
            Theme.Instance.SetColor(TestUtils.C_Color.Name1, TestUtils.C_Theme1.Color1.ValueAlternative);
            yield return null;

            colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.ValueAlternative.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor(), colorBlock.highlightedColor);

            // Update Color2 in theme
            Theme.Instance.SetColor(TestUtils.C_Color.Name2, TestUtils.C_Theme1.Color2.ValueAlternative);
            yield return null;

            colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.ValueAlternative.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.ValueAlternative.HexToColor(), colorBlock.highlightedColor);

            // Restore original colors
            Theme.Instance.SetColor(TestUtils.C_Color.Name1, TestUtils.C_Theme1.Color1.Value);
            Theme.Instance.SetColor(TestUtils.C_Color.Name2, TestUtils.C_Theme1.Color2.Value);
            yield return null;
        }

        [UnityTest] public IEnumerator UpdateColors_WithAlphaOverride_Button_NoLogs() => TestUtils.RunNoLogs(UpdateColors_WithAlphaOverride_Button);
        [UnityTest] public IEnumerator UpdateColors_WithAlphaOverride_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set color with alpha override
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            TestUtils.SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 0.7f);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor().SetA(0.7f), colorBlock.normalColor);

            // Update color in theme - alpha override should be preserved
            Theme.Instance.SetColor(TestUtils.C_Color.Name1, TestUtils.C_Theme1.Color1.ValueAlternative);
            yield return null;

            colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.ValueAlternative.HexToColor().SetA(0.7f), colorBlock.normalColor);

            // Restore original color
            Theme.Instance.SetColor(TestUtils.C_Color.Name1, TestUtils.C_Theme1.Color1.Value);
            yield return null;
        }
    }
}
