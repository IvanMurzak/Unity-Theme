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
        [UnityTest] public IEnumerator SwitchTheme_Button_NoLogs() => TestUtils.RunNoLogs(SwitchTheme_Button);
        [UnityTest] public IEnumerator SwitchTheme_Button() =>
            TestUtils.MultiColorBinder_SwitchTheme<Button, ButtonColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SwitchTheme_AllStates_Button_NoLogs() => TestUtils.RunNoLogs(SwitchTheme_AllStates_Button);
        [UnityTest] public IEnumerator SwitchTheme_AllStates_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            // Set theme 1
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set colors for all states
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByName(colorBinder, 1, TestUtils.C_Color.Name2);
            TestUtils.SetMultiColorByName(colorBinder, 2, TestUtils.C_Color.Name3);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor(), colorBlock.highlightedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color3.Value.HexToColor(), colorBlock.pressedColor);

            // Switch to theme 2
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme2.Name;
            yield return null;

            // Verify colors updated to Theme2
            colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme2.Color1.Value.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme2.Color2.Value.HexToColor(), colorBlock.highlightedColor);
            Assert.AreEqual(TestUtils.C_Theme2.Color3.Value.HexToColor(), colorBlock.pressedColor);
        }

        [UnityTest] public IEnumerator SwitchTheme_WithAlphaOverride_Button_NoLogs() => TestUtils.RunNoLogs(SwitchTheme_WithAlphaOverride_Button);
        [UnityTest] public IEnumerator SwitchTheme_WithAlphaOverride_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            // Set theme 1 with alpha override
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            TestUtils.SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 0.5f);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor().SetA(0.5f), colorBlock.normalColor);

            // Switch to theme 2 - alpha override should be preserved
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme2.Name;
            yield return null;

            colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme2.Color1.Value.HexToColor().SetA(0.5f), colorBlock.normalColor);
        }
    }
}
