using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator Create_Selectable_NoLogs() => TestUtils.RunNoLogs(Create_Selectable);
        [UnityTest]
        public IEnumerator Create_Selectable() =>
            TestUtils.MultiColorBinder_Create<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator Create_Selectable_LabelsInitialized_NoLogs() => TestUtils.RunNoLogs(Create_Selectable_LabelsInitialized);
        [UnityTest]
        public IEnumerator Create_Selectable_LabelsInitialized() =>
            TestUtils.MultiColorBinder_Create_LabelsInitialized<Selectable, SelectableColorBinder>(
                new[] { "Normal", "Highlighted", "Pressed", "Selected", "Disabled" });
    }
}
