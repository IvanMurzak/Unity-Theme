using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools.Utils;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator SetColors_OverrideAlpha_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_OverrideAlpha_Button);
        [UnityTest] public IEnumerator SetColors_OverrideAlpha_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set color and override alpha for first entry (Normal state)
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            TestUtils.SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 0.5f);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor().SetA(0.5f), colorBlock.normalColor);

            // Disable alpha override
            TestUtils.SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: false, alpha: 0.5f);
            colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
        }

        [UnityTest] public IEnumerator SetColors_OverrideAlpha_ByLabel_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_OverrideAlpha_ByLabel_Button);
        [UnityTest] public IEnumerator SetColors_OverrideAlpha_ByLabel_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set color for Normal state
            TestUtils.SetMultiColorByLabel(colorBinder, "Normal", TestUtils.C_Color.Name1);

            // Override alpha using label
            TestUtils.SetMultiAlphaOverrideByLabel(colorBinder, "Normal", overrideAlpha: true, alpha: 0.3f);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor().SetA(0.3f), colorBlock.normalColor);
        }

        [UnityTest] public IEnumerator SetColors_MultipleAlphaOverrides_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_MultipleAlphaOverrides_Button);
        [UnityTest] public IEnumerator SetColors_MultipleAlphaOverrides_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set colors for multiple states
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1); // Normal
            TestUtils.SetMultiColorByName(colorBinder, 1, TestUtils.C_Color.Name2); // Highlighted
            TestUtils.SetMultiColorByName(colorBinder, 2, TestUtils.C_Color.Name3); // Pressed

            // Override alpha for each state with different values
            TestUtils.SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 0.8f);
            TestUtils.SetMultiAlphaOverride(colorBinder, 1, overrideAlpha: true, alpha: 0.6f);
            TestUtils.SetMultiAlphaOverride(colorBinder, 2, overrideAlpha: true, alpha: 0.4f);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor().SetA(0.8f), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor().SetA(0.6f), colorBlock.highlightedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color3.Value.HexToColor().SetA(0.4f), colorBlock.pressedColor);
        }

        [UnityTest] public IEnumerator SetColors_AlphaOverride_InvalidIndex_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_AlphaOverride_InvalidIndex_Button);
        [UnityTest] public IEnumerator SetColors_AlphaOverride_InvalidIndex_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            // Test invalid index for alpha override - expect error logs
            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: 10"));
            Assert.False(colorBinder.SetAlphaOverride(10, true, 0.5f)); // Index out of range

            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: -1"));
            Assert.False(colorBinder.SetAlphaOverride(-1, true, 0.5f)); // Negative index
        }
    }
}
