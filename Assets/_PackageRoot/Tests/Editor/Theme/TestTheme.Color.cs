using System.Collections;
using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.Theme.Tests
{
    public partial class TestTheme : TestBase
    {
        [UnityTest] public IEnumerator SetColor_Undefined_NoLogs() => TestUtils.RunNoLogs(SetColor_Undefined);
        [UnityTest] public IEnumerator SetColor_Undefined()
        {
            LogAssert.Expect(LogType.Error, $"[Theme] SetColor error. Color with name '{TestUtils.C_Color.Name_Undefined}' not found");
            Assert.IsNull(Theme.Instance.SetColor(TestUtils.C_Color.Name_Undefined, Color.red));
            yield return null;
        }
        [UnityTest] public IEnumerator SetColor_NoLogs() => TestUtils.RunNoLogs(SetColor);
        [UnityTest] public IEnumerator SetColor()
        {
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Set color by instance
            Assert.NotNull(Theme.Instance.SetColor(TestUtils.C_Color.Name1, Color.cyan));
            Assert.AreEqual(Color.cyan, Theme.Instance.GetColorByName(TestUtils.C_Color.Name1).Color);

            // Set color by HEX - RGB
            Assert.NotNull(Theme.Instance.SetColor(TestUtils.C_Color.Name1, Color.yellow.ToHexRGB()));
            Assert.AreEqual(Color.yellow, Theme.Instance.GetColorByName(TestUtils.C_Color.Name1).Color);

            // Set color by HEX - RGBA
            Assert.NotNull(Theme.Instance.SetColor(TestUtils.C_Color.Name1, Color.blue.ToHexRGBA()));
            Assert.AreEqual(Color.blue, Theme.Instance.GetColorByName(TestUtils.C_Color.Name1).Color);

            yield return null;
        }
        [UnityTest] public IEnumerator AddColor_NoLogs() => TestUtils.RunNoLogs(AddColor);
        [UnityTest] public IEnumerator AddColor()
        {
            var color = Color.yellow;
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Add default color
            Assert.NotNull(Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor));
            Assert.AreEqual(Theme.DefaultColor, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            // Add color by HEX - RGB
            Assert.NotNull(Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor, color.ToHexRGB()));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            // Add color by HEX - RGBA
            Assert.NotNull(Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor, color.ToHexRGBA()));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            // Add color by instance
            Assert.NotNull(Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor, color));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            yield return null;
        }
        [UnityTest] public IEnumerator SetOrAddColor_NoLogs() => TestUtils.RunNoLogs(SetOrAddColor);
        [UnityTest] public IEnumerator SetOrAddColor()
        {
            var color = Color.yellow;
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Add default color
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_NewColor));
            Assert.AreEqual(Theme.DefaultColor, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);

            // Add color by HEX - RGB
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_NewColor, color.ToHexRGB()));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);

            // Add color by HEX - RGBA
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_NewColor, color.ToHexRGBA()));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);

            // Add color by instance
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_NewColor, color));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);

            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor));

            yield return null;
        }
        [UnityTest] public IEnumerator SetOrAddColor_Undefined_NoLogs() => TestUtils.RunNoLogs(SetOrAddColor_Undefined);
        [UnityTest] public IEnumerator SetOrAddColor_Undefined()
        {
            var color = Color.yellow;
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            Assert.IsFalse(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_Undefined));

            // Add default color
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_Undefined));
            Assert.AreEqual(Theme.DefaultColor, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_Undefined));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined));

            // Add color by HEX - RGB
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_Undefined, color.ToHexRGB()));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_Undefined));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined));

            // Add color by HEX - RGBA
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_Undefined, color.ToHexRGBA()));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_Undefined));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined));

            // Add color by instance
            Assert.NotNull(Theme.Instance.SetOrAddColor(TestUtils.C_Color.Name_Undefined, color));
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined).Color);
            Assert.IsTrue(Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_Undefined));
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_Undefined));

            yield return null;
        }
    }
}