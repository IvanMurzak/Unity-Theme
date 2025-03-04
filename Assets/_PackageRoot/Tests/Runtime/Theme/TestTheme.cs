using System.Collections;
using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine.TestTools;

namespace Unity.Theme.Tests
{
    public partial class TestTheme : TestBase
    {
        [UnitySetUp] public override IEnumerator SetUp() => base.SetUp();
        [UnityTearDown] public override IEnumerator TearDown() => base.TearDown();

        [UnityTest] public IEnumerator AddTheme_NoLogs() => TestUtils.RunNoLogs(AddTheme);
        [UnityTest] public IEnumerator AddTheme()
        {
            Theme.Instance.CurrentThemeName = TestUtils.C_Theme1.Name;

            Assert.NotNull(Theme.Instance.AddTheme(TestUtils.C_ThemeNew.Name, setCurrent: false));
            Assert.AreEqual(TestUtils.C_Theme1.Name, Theme.Instance.CurrentThemeName);

            Theme.Instance.CurrentThemeName = TestUtils.C_ThemeNew.Name;
            Assert.AreEqual(TestUtils.C_ThemeNew.Name, Theme.Instance.CurrentThemeName);

            Assert.AreEqual(1, Theme.Instance.RemoveTheme(TestUtils.C_ThemeNew.Name));
            Assert.AreNotEqual(TestUtils.C_ThemeNew.Name, Theme.Instance.CurrentThemeName);

            var newTheme = Theme.Instance.AddTheme(TestUtils.C_ThemeNew.Name, setCurrent: true);
            Assert.NotNull(newTheme);
            Assert.AreEqual(TestUtils.C_ThemeNew.Name, Theme.Instance.CurrentThemeName);

            Assert.IsTrue(Theme.Instance.RemoveTheme(newTheme));
            Assert.AreNotEqual(TestUtils.C_ThemeNew.Name, Theme.Instance.CurrentThemeName);

            yield return null;
        }
    }
}