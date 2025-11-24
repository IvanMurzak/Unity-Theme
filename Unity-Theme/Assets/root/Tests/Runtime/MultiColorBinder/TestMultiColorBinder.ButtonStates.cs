using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator SelectableStates_AllFiveStates_NoLogs() => TestUtils.RunNoLogs(SelectableStates_AllFiveStates);
        [UnityTest]
        public IEnumerator SelectableStates_AllFiveStates() =>
            TestUtils.MultiColorBinder_SelectableStates_AllFiveStates<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SelectableStates_PreservesColorMultiplier_NoLogs() => TestUtils.RunNoLogs(SelectableStates_PreservesColorMultiplier);
        [UnityTest]
        public IEnumerator SelectableStates_PreservesColorMultiplier() =>
            TestUtils.MultiColorBinder_SelectableStates_PreservesColorMultiplier<Selectable, SelectableColorBinder>(target => target);

        [UnityTest] public IEnumerator SelectableStates_PreservesFadeDuration_NoLogs() => TestUtils.RunNoLogs(SelectableStates_PreservesFadeDuration);
        [UnityTest]
        public IEnumerator SelectableStates_PreservesFadeDuration() =>
            TestUtils.MultiColorBinder_SelectableStates_PreservesFadeDuration<Selectable, SelectableColorBinder>(target => target);

        [UnityTest] public IEnumerator SelectableStates_GetColors_ReturnsAllFive_NoLogs() => TestUtils.RunNoLogs(SelectableStates_GetColors_ReturnsAllFive);
        [UnityTest]
        public IEnumerator SelectableStates_GetColors_ReturnsAllFive() =>
            TestUtils.MultiColorBinder_SelectableStates_GetColors_ReturnsAllFive<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SelectableStates_DifferentAlphaPerState_NoLogs() => TestUtils.RunNoLogs(SelectableStates_DifferentAlphaPerState);
        [UnityTest]
        public IEnumerator SelectableStates_DifferentAlphaPerState() =>
            TestUtils.MultiColorBinder_SelectableStates_DifferentAlphaPerState<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });
    }
}
