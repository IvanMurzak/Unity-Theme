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
        [UnityTest] public IEnumerator SelectableStates_AllFiveStates_NoLogs() => TestUtils.RunNoLogs(SelectableStates_AllFiveStates);
        [UnityTest]
        public IEnumerator SelectableStates_AllFiveStates()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Selectable, SelectableColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set all 5 Selectable states with different colors
            TestUtils.SetMultiColorByLabel(colorBinder, "Normal", TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByLabel(colorBinder, "Highlighted", TestUtils.C_Color.Name2);
            TestUtils.SetMultiColorByLabel(colorBinder, "Pressed", TestUtils.C_Color.Name3);
            TestUtils.SetMultiColorByLabel(colorBinder, "Selected", TestUtils.C_Color.Name4);
            TestUtils.SetMultiColorByLabel(colorBinder, "Disabled", TestUtils.C_Color.Name5);

            var colorBlock = target.colors;
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor(), colorBlock.highlightedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color3.Value.HexToColor(), colorBlock.pressedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color4.Value.HexToColor(), colorBlock.selectedColor);
            Assert.AreEqual(TestUtils.C_Theme1.Color5.Value.HexToColor(), colorBlock.disabledColor);
        }

        [UnityTest] public IEnumerator SelectableStates_PreservesColorMultiplier_NoLogs() => TestUtils.RunNoLogs(SelectableStates_PreservesColorMultiplier);
        [UnityTest]
        public IEnumerator SelectableStates_PreservesColorMultiplier()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Selectable, SelectableColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set custom colorMultiplier
            var colorBlock = target.colors;
            colorBlock.colorMultiplier = 2.5f;
            target.colors = colorBlock;

            // Change a color
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);

            // Verify colorMultiplier is preserved
            colorBlock = target.colors;
            Assert.AreEqual(2.5f, colorBlock.colorMultiplier);
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
        }

        [UnityTest] public IEnumerator SelectableStates_PreservesFadeDuration_NoLogs() => TestUtils.RunNoLogs(SelectableStates_PreservesFadeDuration);
        [UnityTest]
        public IEnumerator SelectableStates_PreservesFadeDuration()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Selectable, SelectableColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set custom fadeDuration
            var colorBlock = target.colors;
            colorBlock.fadeDuration = 0.5f;
            target.colors = colorBlock;

            // Change a color
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);

            // Verify fadeDuration is preserved
            colorBlock = target.colors;
            Assert.AreEqual(0.5f, colorBlock.fadeDuration);
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
        }

        [UnityTest] public IEnumerator SelectableStates_GetColors_ReturnsAllFive_NoLogs() => TestUtils.RunNoLogs(SelectableStates_GetColors_ReturnsAllFive);
        [UnityTest]
        public IEnumerator SelectableStates_GetColors_ReturnsAllFive()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Selectable, SelectableColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set all 5 colors
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByName(colorBinder, 1, TestUtils.C_Color.Name2);
            TestUtils.SetMultiColorByName(colorBinder, 2, TestUtils.C_Color.Name3);
            TestUtils.SetMultiColorByName(colorBinder, 3, TestUtils.C_Color.Name4);
            TestUtils.SetMultiColorByName(colorBinder, 4, TestUtils.C_Color.Name5);

            // Get colors and verify all are correct
            var colors = colorBinder.GetColors();
            Assert.AreEqual(5, colors.Length);
            Assert.AreEqual(TestUtils.C_Theme1.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(TestUtils.C_Theme1.Color2.Value.HexToColor(), colors[1]);
            Assert.AreEqual(TestUtils.C_Theme1.Color3.Value.HexToColor(), colors[2]);
            Assert.AreEqual(TestUtils.C_Theme1.Color4.Value.HexToColor(), colors[3]);
            Assert.AreEqual(TestUtils.C_Theme1.Color5.Value.HexToColor(), colors[4]);
        }

        [UnityTest] public IEnumerator SelectableStates_DifferentAlphaPerState_NoLogs() => TestUtils.RunNoLogs(SelectableStates_DifferentAlphaPerState);
        [UnityTest]
        public IEnumerator SelectableStates_DifferentAlphaPerState()
        {
            var colorBinder = TestUtils.CreateGenericMultiColorBinder<Selectable, SelectableColorBinder>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set same color for all states but with different alpha values
            TestUtils.SetMultiColorByName(colorBinder, 0, TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByName(colorBinder, 1, TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByName(colorBinder, 2, TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByName(colorBinder, 3, TestUtils.C_Color.Name1);
            TestUtils.SetMultiColorByName(colorBinder, 4, TestUtils.C_Color.Name1);

            // Override alpha for each state with different values
            TestUtils.SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 1.0f);   // Normal - full opacity
            TestUtils.SetMultiAlphaOverride(colorBinder, 1, overrideAlpha: true, alpha: 0.9f);   // Highlighted
            TestUtils.SetMultiAlphaOverride(colorBinder, 2, overrideAlpha: true, alpha: 0.8f);   // Pressed
            TestUtils.SetMultiAlphaOverride(colorBinder, 3, overrideAlpha: true, alpha: 0.7f);   // Selected
            TestUtils.SetMultiAlphaOverride(colorBinder, 4, overrideAlpha: true, alpha: 0.5f);   // Disabled - half opacity

            var colorBlock = target.colors;
            var baseColor = TestUtils.C_Theme1.Color1.Value.HexToColor();
            Assert.AreEqual(baseColor.SetA(1.0f), colorBlock.normalColor);
            Assert.AreEqual(baseColor.SetA(0.9f), colorBlock.highlightedColor);
            Assert.AreEqual(baseColor.SetA(0.8f), colorBlock.pressedColor);
            Assert.AreEqual(baseColor.SetA(0.7f), colorBlock.selectedColor);
            Assert.AreEqual(baseColor.SetA(0.5f), colorBlock.disabledColor);
        }
    }
}
