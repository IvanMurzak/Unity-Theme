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
        [UnityTest] public IEnumerator SetColors_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_Button);
        [UnityTest] public IEnumerator SetColors_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set colors for all 5 button states
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1); // Normal
            TestUtils.SetMultiColorByName(colorBinder, 1, TestUtils.C_Color.Name2); // Highlighted
            TestUtils.SetMultiColorByName(colorBinder, 2, TestUtils.C_Color.Name3); // Pressed
            TestUtils.SetMultiColorByName(colorBinder, 3, TestUtils.C_Color.Name4); // Selected
            TestUtils.SetMultiColorByName(colorBinder, 4, TestUtils.C_Color.Name5); // Disabled

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor(), colorBlock.highlightedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color3.Value.HexToColor(), colorBlock.pressedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color4.Value.HexToColor(), colorBlock.selectedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color5.Value.HexToColor(), colorBlock.disabledColor);
        }

        [UnityTest] public IEnumerator SetColors_ByIndex_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_ByIndex_Button);
        [UnityTest] public IEnumerator SetColors_ByIndex_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Test setting by index
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            var colors = colorBinder.GetColors();
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colors[0]);
        }

        [UnityTest] public IEnumerator SetColors_ByLabel_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_ByLabel_Button);
        [UnityTest] public IEnumerator SetColors_ByLabel_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Test setting by label
            TestUtils.SetMultiColorByLabel(colorBinder, "Normal", TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByLabel(colorBinder, "Highlighted", TestUtils.C_Color.Name2);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor(), colorBlock.highlightedColor);
        }

        [UnityTest] public IEnumerator SetColors_InvalidIndex_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_InvalidIndex_Button);
        [UnityTest] public IEnumerator SetColors_InvalidIndex_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Test invalid index - expect error logs
            var colorData = Theme.Instance.GetColorByName(TestUtils.C_Color.Name1);

            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: 10"));
            Assert.False(colorBinder.SetColor(10, colorData)); // Index out of range

            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: -1"));
            Assert.False(colorBinder.SetColor(-1, colorData)); // Negative index
        }

        [UnityTest] public IEnumerator SetColors_InvalidLabel_Button_NoLogs() => TestUtils.RunNoLogs(SetColors_InvalidLabel_Button);
        [UnityTest] public IEnumerator SetColors_InvalidLabel_Button()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Test invalid label - expect error log
            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Color entry with label 'InvalidLabel' not found"));
            Assert.False(colorBinder.SetColorByLabel("InvalidLabel", TestUtils.C_Color.Name1));
        }
    }
}
