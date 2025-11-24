using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator SetColors_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_Selectable);
        [UnityTest]
        public IEnumerator SetColors_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_Button<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SetColors_ByIndex_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_ByIndex_Selectable);
        [UnityTest]
        public IEnumerator SetColors_ByIndex_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_ByIndex<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SetColors_ByLabel_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_ByLabel_Selectable);
        [UnityTest]
        public IEnumerator SetColors_ByLabel_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_ByLabel<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            }, "Normal", "Highlighted");

        [UnityTest] public IEnumerator SetColors_InvalidIndex_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_InvalidIndex_Selectable);
        [UnityTest]
        public IEnumerator SetColors_InvalidIndex_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_InvalidIndex<Selectable, SelectableColorBinder>();

        [UnityTest] public IEnumerator SetColors_InvalidLabel_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_InvalidLabel_Selectable);
        [UnityTest]
        public IEnumerator SetColors_InvalidLabel_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_InvalidLabel<Selectable, SelectableColorBinder>();
    }
}
