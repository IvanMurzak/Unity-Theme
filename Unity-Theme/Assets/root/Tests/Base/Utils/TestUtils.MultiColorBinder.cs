using NUnit.Framework;
using UnityEngine;
using Unity.Theme.Binders;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        /// <summary>
        /// Create a multi-color binder component with its target component
        /// </summary>
        public static B CreateMultiColorBinder<T, B>(out T target, GameObject gameObject = null)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            if (gameObject == null)
                gameObject = new GameObject($"_{gameObjectCounter++}");

            target = gameObject.AddComponent<T>();
            return gameObject.AddComponent<B>();
        }

        /// <summary>
        /// Create a generic multi-color binder and assert basic properties
        /// </summary>
        public static GenericMultiColorBinder<T> CreateGenericMultiColorBinder<T, B>(out T target)
            where T : Component
            where B : GenericMultiColorBinder<T>
        {
            var colorBinder = CreateMultiColorBinder<T, B>(out target);
            Assert.NotNull(target);
            Assert.NotNull(colorBinder);
            Assert.NotNull(colorBinder.Target);
            Assert.AreEqual(target, colorBinder.Target);
            return colorBinder;
        }

        /// <summary>
        /// Set color for a specific entry by index and verify it was applied
        /// </summary>
        public static void SetMultiColor(BaseMultiColorBinder colorBinder, int index, ColorData colorData)
        {
            Assert.True(colorBinder.SetColor(index, colorData),
                $"Failed to set color at index {index}");

            var colors = colorBinder.GetColors();
            Assert.NotNull(colors);
            Assert.Greater(colors.Length, index, $"Color array length {colors.Length} <= index {index}");

            var targetColor = colorData.Color;
            if (colorBinder.IsAlphaOverridden(index))
                targetColor = targetColor.SetA(colorBinder.GetAlphaOverrideValue(index));

            Assert.AreEqual(targetColor, colors[index],
                $"Color mismatch at index {index}");
        }

        /// <summary>
        /// Set color by name for a specific entry by index
        /// </summary>
        public static void SetMultiColorByName(BaseMultiColorBinder colorBinder, int index, string colorName)
        {
            Assert.True(colorBinder.SetColorByName(index, colorName),
                $"Failed to set color '{colorName}' at index {index}");

            var colors = colorBinder.GetColors();
            var targetColor = Theme.Instance.GetColorByName(colorName).Color;
            if (colorBinder.IsAlphaOverridden(index))
                targetColor = targetColor.SetA(colorBinder.GetAlphaOverrideValue(index));

            Assert.AreEqual(targetColor, colors[index],
                $"Color mismatch at index {index} for color '{colorName}'");
        }

        /// <summary>
        /// Set color by label for a specific entry
        /// </summary>
        public static void SetMultiColorByLabel(BaseMultiColorBinder colorBinder, string label, string colorName)
        {
            Assert.True(colorBinder.SetColorByLabel(label, colorName),
                $"Failed to set color '{colorName}' for label '{label}'");

            int index = colorBinder.GetIndexByLabel(label);
            Assert.GreaterOrEqual(index, 0, $"Label '{label}' not found");

            var colors = colorBinder.GetColors();
            var targetColor = Theme.Instance.GetColorByName(colorName).Color;
            if (colorBinder.IsAlphaOverridden(index))
                targetColor = targetColor.SetA(colorBinder.GetAlphaOverrideValue(index));

            Assert.AreEqual(targetColor, colors[index],
                $"Color mismatch for label '{label}' (index {index})");
        }

        /// <summary>
        /// Set alpha override for a specific entry and verify
        /// </summary>
        public static void SetMultiAlphaOverride(BaseMultiColorBinder colorBinder, int index, bool overrideAlpha, float alpha)
        {
            Assert.True(colorBinder.SetAlphaOverride(index, overrideAlpha, alpha),
                $"Failed to set alpha override at index {index}");

            Assert.AreEqual(overrideAlpha, colorBinder.IsAlphaOverridden(index),
                $"Alpha override mismatch at index {index}");

            var expected = overrideAlpha ? alpha : 1.0f;
            Assert.AreEqual(expected, colorBinder.GetAlphaOverrideValue(index),
                $"Alpha value mismatch at index {index}, overrideAlpha={overrideAlpha}, alpha={alpha}");
        }

        /// <summary>
        /// Set alpha override by label for a specific entry
        /// </summary>
        public static void SetMultiAlphaOverrideByLabel(BaseMultiColorBinder colorBinder, string label, bool overrideAlpha, float alpha)
        {
            int index = colorBinder.GetIndexByLabel(label);
            Assert.GreaterOrEqual(index, 0, $"Label '{label}' not found");

            Assert.True(colorBinder.SetAlphaOverrideByLabel(label, overrideAlpha, alpha),
                $"Failed to set alpha override for label '{label}'");

            Assert.AreEqual(overrideAlpha, colorBinder.IsAlphaOverridden(index),
                $"Alpha override mismatch for label '{label}'");

            var expected = overrideAlpha ? alpha : 1.0f;
            Assert.AreEqual(expected, colorBinder.GetAlphaOverrideValue(index),
                $"Alpha value mismatch for label '{label}'");
        }

        /// <summary>
        /// Verify all colors in a multi-color binder match expected values
        /// </summary>
        public static void AssertMultiColors(BaseMultiColorBinder colorBinder, params Color[] expectedColors)
        {
            var actualColors = colorBinder.GetColors();
            Assert.NotNull(actualColors, "GetColors() returned null");
            Assert.AreEqual(expectedColors.Length, actualColors.Length,
                $"Color array length mismatch: expected {expectedColors.Length}, got {actualColors.Length}");

            for (int i = 0; i < expectedColors.Length; i++)
            {
                Assert.AreEqual(expectedColors[i], actualColors[i],
                    $"Color mismatch at index {i}");
            }
        }

        /// <summary>
        /// Verify that a multi-color binder has the expected number of color entries
        /// </summary>
        public static void AssertColorEntryCount(BaseMultiColorBinder colorBinder, int expectedCount)
        {
            int actualCount = colorBinder.GetColorEntryCount();
            Assert.AreEqual(expectedCount, actualCount,
                $"Color entry count mismatch: expected {expectedCount}, got {actualCount}");
        }
    }
}
