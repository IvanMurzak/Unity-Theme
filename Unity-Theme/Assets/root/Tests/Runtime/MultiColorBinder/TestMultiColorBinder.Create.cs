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
        [UnityTest] public IEnumerator Create_Selectable_NoLogs() => TestUtils.RunNoLogs(Create_Selectable);
        [UnityTest]
        public IEnumerator Create_Selectable()
        {
            var binder = TestUtils.CreateGenericMultiColorBinder<Selectable, SelectableColorBinder>(out var target);

            // Verify SelectableColorBinder has 5 color entries (Normal, Highlighted, Pressed, Selected, Disabled)
            TestUtils.AssertColorEntryCount(binder, 5);

            // Verify all colors are initialized
            var colors = binder.GetColors();
            Assert.NotNull(colors);
            Assert.AreEqual(5, colors.Length);

            yield return null;
        }

        [UnityTest] public IEnumerator Create_Selectable_LabelsInitialized_NoLogs() => TestUtils.RunNoLogs(Create_Selectable_LabelsInitialized);
        [UnityTest]
        public IEnumerator Create_Selectable_LabelsInitialized()
        {
            var binder = TestUtils.CreateGenericMultiColorBinder<Selectable, SelectableColorBinder>(out var target);

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
