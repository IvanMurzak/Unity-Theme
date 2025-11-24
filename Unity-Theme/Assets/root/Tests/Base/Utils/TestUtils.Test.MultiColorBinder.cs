using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Theme.Binders;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        /// <summary>
        /// Generic test pattern for setting colors in a multi-color binder
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_Button<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set different colors for each entry
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiColorByName(colorBinder, 1, C_Color.Name2);
            SetMultiColorByName(colorBinder, 2, C_Color.Name3);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[1]);
            Assert.AreEqual(C_Theme1.Color3.Value.HexToColor(), colors[2]);
        }

        /// <summary>
        /// Generic test pattern for setting colors with alpha override in a multi-color binder
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_OverrideAlpha<T, B>(Func<T, Color[]> getter, float alpha = 0.5f)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set color for first entry
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);

            // Apply alpha override
            SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha);
            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor().SetA(alpha), colors[0]);

            // Disable alpha override
            SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: false, alpha);
            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
        }

        /// <summary>
        /// Generic test pattern for switching colors in a multi-color binder
        /// </summary>
        public static IEnumerator MultiColorBinder_SwitchColor<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set first color
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);

            // Switch to second color
            SetMultiColorByName(colorBinder, 0, C_Color.Name2);
            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[0]);
        }

        /// <summary>
        /// Generic test pattern for updating colors dynamically in a multi-color binder
        /// </summary>
        public static IEnumerator MultiColorBinder_UpdateColor<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set initial color
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);

            // Update the color in the theme
            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.ValueAlternative);
            yield return null;
            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.ValueAlternative.HexToColor(), colors[0]);

            // Switch to second color
            SetMultiColorByName(colorBinder, 0, C_Color.Name2);
            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[0]);

            // Update the second color
            Theme.Instance.SetColor(C_Color.Name2, C_Theme1.Color2.ValueAlternative);
            yield return null;
            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color2.ValueAlternative.HexToColor(), colors[0]);

            // Restore original color
            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.Value);
            yield return null;
        }

        /// <summary>
        /// Generic test pattern for theme switching in a multi-color binder
        /// </summary>
        public static IEnumerator MultiColorBinder_SwitchTheme<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set color in Theme1
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);

            // Switch to Theme2
            Theme.Instance.CurrentThemeName = C_Theme2.Name;
            colors = getter(target);
            Assert.AreEqual(C_Theme2.Color1.Value.HexToColor(), colors[0]);

            // Set different color in Theme2
            SetMultiColorByName(colorBinder, 0, C_Color.Name2);
            colors = getter(target);
            Assert.AreEqual(C_Theme2.Color2.Value.HexToColor(), colors[0]);
        }

        /// <summary>
        /// Generic test pattern for basic creation and validation
        /// </summary>
        public static IEnumerator MultiColorBinder_Create<T, B>(Func<T, Color[]> getter, int expectedColorCount = 5)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var binder = CreateGenericMultiColorBinder<T, B>(out var target);

            // Verify color entry count
            AssertColorEntryCount(binder, expectedColorCount);

            // Verify all colors are initialized
            var colors = binder.GetColors();
            Assert.NotNull(colors);
            Assert.AreEqual(expectedColorCount, colors.Length);

            yield return null;
        }

        /// <summary>
        /// Generic test pattern for label initialization validation
        /// </summary>
        public static IEnumerator MultiColorBinder_Create_LabelsInitialized<T, B>(string[] labels)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var binder = CreateGenericMultiColorBinder<T, B>(out var target);

            // Verify labels are correctly initialized
            for (int i = 0; i < labels.Length; i++)
            {
                Assert.AreEqual(i, binder.GetIndexByLabel(labels[i]),
                    $"Label '{labels[i]}' should be at index {i}");
            }

            // Verify invalid label returns -1
            Assert.AreEqual(-1, binder.GetIndexByLabel("InvalidLabel"));

            yield return null;
        }

        /// <summary>
        /// Generic test pattern for simple index-based color setting
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_ByIndex<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Test setting by index
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            var colors = colorBinder.GetColors();
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
        }

        /// <summary>
        /// Generic test pattern for simple label-based color setting
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_ByLabel<T, B>(Func<T, Color[]> getter, string label1, string label2)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Test setting by label
            SetMultiColorByLabel(colorBinder, label1, C_Color.Name1);
            SetMultiColorByLabel(colorBinder, label2, C_Color.Name2);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[1]);
        }

        /// <summary>
        /// Generic test pattern for invalid index error handling
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_InvalidIndex<T, B>()
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Test invalid index - expect error logs
            var colorData = Theme.Instance.GetColorByName(C_Color.Name1);

            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: 10"));
            Assert.False(colorBinder.SetColor(10, colorData)); // Index out of range

            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: -1"));
            Assert.False(colorBinder.SetColor(-1, colorData)); // Negative index
        }

        /// <summary>
        /// Generic test pattern for invalid label error handling
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_InvalidLabel<T, B>()
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Test invalid label - expect error log
            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Color entry with label 'InvalidLabel' not found"));
            Assert.False(colorBinder.SetColorByLabel("InvalidLabel", C_Color.Name1));
        }

        /// <summary>
        /// Generic test pattern for alpha override by label
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_OverrideAlpha_ByLabel<T, B>(Func<T, Color[]> getter, string label, float alpha = 0.3f)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set color for specified label
            SetMultiColorByLabel(colorBinder, label, C_Color.Name1);

            // Override alpha using label
            SetMultiAlphaOverrideByLabel(colorBinder, label, overrideAlpha: true, alpha);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor().SetA(alpha), colors[0]);
        }

        /// <summary>
        /// Generic test pattern for multiple alpha overrides
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_MultipleAlphaOverrides<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set colors for multiple states
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiColorByName(colorBinder, 1, C_Color.Name2);
            SetMultiColorByName(colorBinder, 2, C_Color.Name3);

            // Override alpha for each state with different values
            SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 0.8f);
            SetMultiAlphaOverride(colorBinder, 1, overrideAlpha: true, alpha: 0.6f);
            SetMultiAlphaOverride(colorBinder, 2, overrideAlpha: true, alpha: 0.4f);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor().SetA(0.8f), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor().SetA(0.6f), colors[1]);
            Assert.AreEqual(C_Theme1.Color3.Value.HexToColor().SetA(0.4f), colors[2]);
        }

        /// <summary>
        /// Generic test pattern for invalid index alpha override error handling
        /// </summary>
        public static IEnumerator MultiColorBinder_SetColors_AlphaOverride_InvalidIndex<T, B>()
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            // Test invalid index for alpha override - expect error logs
            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: 10"));
            Assert.False(colorBinder.SetAlphaOverride(10, true, 0.5f)); // Index out of range

            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex(@"\[Theme\] Invalid color entry index: -1"));
            Assert.False(colorBinder.SetAlphaOverride(-1, true, 0.5f)); // Negative index
        }

        /// <summary>
        /// Generic test pattern for all 5 Selectable states with different colors
        /// </summary>
        public static IEnumerator MultiColorBinder_SelectableStates_AllFiveStates<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set all 5 Selectable states with different colors
            SetMultiColorByLabel(colorBinder, "Normal", C_Color.Name1);
            SetMultiColorByLabel(colorBinder, "Highlighted", C_Color.Name2);
            SetMultiColorByLabel(colorBinder, "Pressed", C_Color.Name3);
            SetMultiColorByLabel(colorBinder, "Selected", C_Color.Name4);
            SetMultiColorByLabel(colorBinder, "Disabled", C_Color.Name5);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[1]);
            Assert.AreEqual(C_Theme1.Color3.Value.HexToColor(), colors[2]);
            Assert.AreEqual(C_Theme1.Color4.Value.HexToColor(), colors[3]);
            Assert.AreEqual(C_Theme1.Color5.Value.HexToColor(), colors[4]);
        }

        /// <summary>
        /// Generic test pattern for preserving ColorBlock.colorMultiplier
        /// </summary>
        public static IEnumerator MultiColorBinder_SelectableStates_PreservesColorMultiplier<T, B>(Func<T, Selectable> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set custom colorMultiplier
            var selectable = getter(target);
            var colorBlock = selectable.colors;
            colorBlock.colorMultiplier = 2.5f;
            selectable.colors = colorBlock;

            // Change a color
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);

            // Verify colorMultiplier is preserved
            colorBlock = selectable.colors;
            Assert.AreEqual(2.5f, colorBlock.colorMultiplier);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
        }

        /// <summary>
        /// Generic test pattern for preserving ColorBlock.fadeDuration
        /// </summary>
        public static IEnumerator MultiColorBinder_SelectableStates_PreservesFadeDuration<T, B>(Func<T, Selectable> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set custom fadeDuration
            var selectable = getter(target);
            var colorBlock = selectable.colors;
            colorBlock.fadeDuration = 0.5f;
            selectable.colors = colorBlock;

            // Change a color
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);

            // Verify fadeDuration is preserved
            colorBlock = selectable.colors;
            Assert.AreEqual(0.5f, colorBlock.fadeDuration);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colorBlock.normalColor);
        }

        /// <summary>
        /// Generic test pattern for verifying GetColors returns all entries
        /// </summary>
        public static IEnumerator MultiColorBinder_SelectableStates_GetColors_ReturnsAllFive<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set all 5 colors
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiColorByName(colorBinder, 1, C_Color.Name2);
            SetMultiColorByName(colorBinder, 2, C_Color.Name3);
            SetMultiColorByName(colorBinder, 3, C_Color.Name4);
            SetMultiColorByName(colorBinder, 4, C_Color.Name5);

            // Get colors and verify all are correct
            var colors = colorBinder.GetColors();
            Assert.AreEqual(5, colors.Length);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[1]);
            Assert.AreEqual(C_Theme1.Color3.Value.HexToColor(), colors[2]);
            Assert.AreEqual(C_Theme1.Color4.Value.HexToColor(), colors[3]);
            Assert.AreEqual(C_Theme1.Color5.Value.HexToColor(), colors[4]);
        }

        /// <summary>
        /// Generic test pattern for different alpha values per state
        /// </summary>
        public static IEnumerator MultiColorBinder_SelectableStates_DifferentAlphaPerState<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set same color for all states but with different alpha values
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiColorByName(colorBinder, 1, C_Color.Name1);
            SetMultiColorByName(colorBinder, 2, C_Color.Name1);
            SetMultiColorByName(colorBinder, 3, C_Color.Name1);
            SetMultiColorByName(colorBinder, 4, C_Color.Name1);

            // Override alpha for each state with different values
            SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 1.0f);   // Normal - full opacity
            SetMultiAlphaOverride(colorBinder, 1, overrideAlpha: true, alpha: 0.9f);   // Highlighted
            SetMultiAlphaOverride(colorBinder, 2, overrideAlpha: true, alpha: 0.8f);   // Pressed
            SetMultiAlphaOverride(colorBinder, 3, overrideAlpha: true, alpha: 0.7f);   // Selected
            SetMultiAlphaOverride(colorBinder, 4, overrideAlpha: true, alpha: 0.5f);   // Disabled - half opacity

            var colors = getter(target);
            var baseColor = C_Theme1.Color1.Value.HexToColor();
            Assert.AreEqual(baseColor.SetA(1.0f), colors[0]);
            Assert.AreEqual(baseColor.SetA(0.9f), colors[1]);
            Assert.AreEqual(baseColor.SetA(0.8f), colors[2]);
            Assert.AreEqual(baseColor.SetA(0.7f), colors[3]);
            Assert.AreEqual(baseColor.SetA(0.5f), colors[4]);
        }

        /// <summary>
        /// Generic test pattern for updating multiple color entries
        /// </summary>
        public static IEnumerator MultiColorBinder_UpdateColors_MultipleEntries<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set initial colors
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiColorByName(colorBinder, 1, C_Color.Name2);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[1]);

            // Update Color1 in theme
            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.ValueAlternative);
            yield return null;

            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.ValueAlternative.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[1]);

            // Update Color2 in theme
            Theme.Instance.SetColor(C_Color.Name2, C_Theme1.Color2.ValueAlternative);
            yield return null;

            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.ValueAlternative.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.ValueAlternative.HexToColor(), colors[1]);

            // Restore original colors
            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.Value);
            Theme.Instance.SetColor(C_Color.Name2, C_Theme1.Color2.Value);
            yield return null;
        }

        /// <summary>
        /// Generic test pattern for updating colors with alpha override
        /// </summary>
        public static IEnumerator MultiColorBinder_UpdateColors_WithAlphaOverride<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set color with alpha override
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 0.7f);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor().SetA(0.7f), colors[0]);

            // Update color in theme - alpha override should be preserved
            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.ValueAlternative);
            yield return null;

            colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.ValueAlternative.HexToColor().SetA(0.7f), colors[0]);

            // Restore original color
            Theme.Instance.SetColor(C_Color.Name1, C_Theme1.Color1.Value);
            yield return null;
        }

        /// <summary>
        /// Generic test pattern for theme switching across multiple states
        /// </summary>
        public static IEnumerator MultiColorBinder_SwitchTheme_AllStates<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            // Set theme 1
            Theme.Instance.CurrentThemeName = C_Theme1.Name;

            // Set colors for all states
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiColorByName(colorBinder, 1, C_Color.Name2);
            SetMultiColorByName(colorBinder, 2, C_Color.Name3);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme1.Color2.Value.HexToColor(), colors[1]);
            Assert.AreEqual(C_Theme1.Color3.Value.HexToColor(), colors[2]);

            // Switch to theme 2
            Theme.Instance.CurrentThemeName = C_Theme2.Name;
            yield return null;

            // Verify colors updated to Theme2
            colors = getter(target);
            Assert.AreEqual(C_Theme2.Color1.Value.HexToColor(), colors[0]);
            Assert.AreEqual(C_Theme2.Color2.Value.HexToColor(), colors[1]);
            Assert.AreEqual(C_Theme2.Color3.Value.HexToColor(), colors[2]);
        }

        /// <summary>
        /// Generic test pattern for theme switching with alpha override
        /// </summary>
        public static IEnumerator MultiColorBinder_SwitchTheme_WithAlphaOverride<T, B>(Func<T, Color[]> getter)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateGenericMultiColorBinder<T, B>(out var target);
            yield return null;

            // Set theme 1 with alpha override
            Theme.Instance.CurrentThemeName = C_Theme1.Name;
            SetMultiColorByName(colorBinder, 0, C_Color.Name1);
            SetMultiAlphaOverride(colorBinder, 0, overrideAlpha: true, alpha: 0.5f);

            var colors = getter(target);
            Assert.AreEqual(C_Theme1.Color1.Value.HexToColor().SetA(0.5f), colors[0]);

            // Switch to theme 2 - alpha override should be preserved
            Theme.Instance.CurrentThemeName = C_Theme2.Name;
            yield return null;

            colors = getter(target);
            Assert.AreEqual(C_Theme2.Color1.Value.HexToColor().SetA(0.5f), colors[0]);
        }
    }
}
