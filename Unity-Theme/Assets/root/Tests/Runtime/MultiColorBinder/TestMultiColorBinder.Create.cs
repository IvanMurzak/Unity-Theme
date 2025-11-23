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
        [UnityTest] public IEnumerator Create_Button_NoLogs() => TestUtils.RunNoLogs(Create_Button);
        [UnityTest] public IEnumerator Create_Button()
        {
            var binder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);

            // Verify ButtonColorBinder has 5 color entries (Normal, Highlighted, Pressed, Selected, Disabled)
            TestUtils.AssertColorEntryCount(binder, 5);

            // Verify all colors are initialized
            var colors = binder.GetColors();
            Assert.NotNull(colors);
            Assert.AreEqual(5, colors.Length);

            yield return null;
        }

        [UnityTest] public IEnumerator Create_Button_LabelsInitialized_NoLogs() => TestUtils.RunNoLogs(Create_Button_LabelsInitialized);
        [UnityTest] public IEnumerator Create_Button_LabelsInitialized()
        {
            var binder = TestUtils.CreateGenericMultiColorBinder<Button, ButtonColorBinder>(out var target);

            // Verify labels are correctly initialized
            Assert.AreEqual(0, binder.GetIndexByLabel("Normal"));
            Assert.AreEqual(1, binder.GetIndexByLabel("Highlighted"));
            Assert.AreEqual(2, binder.GetIndexByLabel("Pressed"));
            Assert.AreEqual(3, binder.GetIndexByLabel("Selected"));
            Assert.AreEqual(4, binder.GetIndexByLabel("Disabled"));

            // Verify invalid label returns -1
            Assert.AreEqual(-1, binder.GetIndexByLabel("InvalidLabel"));

            yield return null;
        }
    }
}
