using NUnit.Framework;
using UnityEngine;
using Unity.Theme.Binders;

namespace Unity.Theme.Tests.Editor
{
    public static partial class TestUtils
    {
        public static B CreateColorBinder<T, B>(out T target, GameObject gameObject = null) where T : Component where B : GenericColorBinder<T>
        {
            if (gameObject == null)
                gameObject = new GameObject();

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
            Assert.AreEqual(colorData.Color, colorBinder.GetColor().Value);
        }
    }
}