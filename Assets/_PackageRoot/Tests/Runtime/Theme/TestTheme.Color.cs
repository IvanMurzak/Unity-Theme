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
            Theme.Instance.SetColor(TestUtils.C_Color.Name_Undefined, Color.red);
            yield return null;
        }
        [UnityTest] public IEnumerator SetColor_NoLogs() => TestUtils.RunNoLogs(SetColor);
        [UnityTest] public IEnumerator SetColor()
        {
            var color = Color.cyan;
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;
            Theme.Instance.SetColor(TestUtils.C_Color.Name1, color);
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name1).Color);
            yield return null;
        }
        [UnityTest] public IEnumerator AddColor_NoLogs() => TestUtils.RunNoLogs(AddColor);
        [UnityTest] public IEnumerator AddColor()
        {
            var color = Color.yellow;
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            // Add default color
            Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor);
            Assert.AreEqual(Theme.DefaultColor, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor);
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            // Add color by HEX - RGB
            Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor, color.ToHexRGB());
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor);
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            // Add color by HEX - RGBA
            Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor, color.ToHexRGBA());
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor);
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            // Add color by instance
            Theme.Instance.AddColor(TestUtils.C_Color.Name_NewColor, color);
            Assert.AreEqual(color, Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor).Color);
            Theme.Instance.RemoveColorByName(TestUtils.C_Color.Name_NewColor);
            Assert.IsNull(Theme.Instance.GetColorByName(TestUtils.C_Color.Name_NewColor));

            yield return null;
        }
    }
}