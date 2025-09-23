using NUnit.Framework;
using UnityEngine;
using Unity.Theme.Binders;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        static uint gameObjectCounter = 0;

        public static B CreateColorBinder<T, B>(out T target, GameObject gameObject = null) where T : Component where B : GenericColorBinder<T>
        {
            if (gameObject == null)
                gameObject = new GameObject($"_{gameObjectCounter++}");

            target = gameObject.AddComponent<T>();
            return gameObject.AddComponent<B>();
        }
        public static GenericColorBinder<T> CreateGenericColorBinder<T, B>(out T target) where T : Component where B : GenericColorBinder<T>
        {
            var colorBinder = CreateColorBinder<T, B>(out target);
            Assert.NotNull(target);
            Assert.NotNull(colorBinder);
            Assert.NotNull(colorBinder.Target);
            Assert.AreEqual(target, colorBinder.Target);
            return colorBinder;
        }
        public static void SetColor(BaseColorBinder colorBinder, ColorData colorData)
        {
            Assert.True(colorBinder.SetColor(colorData));

            var targetColor = colorData.Color;
            if (colorBinder.IsAlphaOverridden())
                targetColor = targetColor.SetA(colorBinder.GetAlphaOverrideValue());
            Assert.AreEqual(targetColor, colorBinder.GetColor().Value);
        }
        public static void SetColorByName(BaseColorBinder colorBinder, string name)
        {
            Assert.True(colorBinder.SetColorByName(name));

            var targetColor = Theme.Instance.GetColorByName(name).Color;
            if (colorBinder.IsAlphaOverridden())
                targetColor = targetColor.SetA(colorBinder.GetAlphaOverrideValue());
            Assert.AreEqual(targetColor, colorBinder.GetColor().Value);
        }
        public static void SetAlphaOverride(BaseColorBinder colorBinder, bool overrideAlpha, float alpha)
        {
            Assert.True(colorBinder.SetAlphaOverride(overrideAlpha, alpha));
            Assert.AreEqual(overrideAlpha, colorBinder.IsAlphaOverridden());
            var expected = overrideAlpha
                ? colorBinder.GetAlphaOverrideValue()
                : 1.0f;
            Assert.AreEqual(expected, colorBinder.GetAlphaOverrideValue(), $"{colorBinder.GetType().Name} alpha override value, overrideAlpha={overrideAlpha}, alpha={alpha}");
        }
    }
}