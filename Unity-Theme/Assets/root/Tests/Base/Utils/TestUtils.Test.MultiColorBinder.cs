using System.Collections;
using System;
using UnityEngine;
using Unity.Theme.Binders;
using NUnit.Framework;

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
    }
}
