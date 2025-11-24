using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator UpdateColors_Selectable_NoLogs() => TestUtils.RunNoLogs(UpdateColors_Selectable);
        [UnityTest]
        public IEnumerator UpdateColors_Selectable() =>
            TestUtils.MultiColorBinder_UpdateColor<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator UpdateColors_MultipleEntries_Selectable_NoLogs() => TestUtils.RunNoLogs(UpdateColors_MultipleEntries_Selectable);
        [UnityTest]
        public IEnumerator UpdateColors_MultipleEntries_Selectable() =>
            TestUtils.MultiColorBinder_UpdateColors_MultipleEntries<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator UpdateColors_WithAlphaOverride_Selectable_NoLogs() => TestUtils.RunNoLogs(UpdateColors_WithAlphaOverride_Selectable);
        [UnityTest]
        public IEnumerator UpdateColors_WithAlphaOverride_Selectable() =>
            TestUtils.MultiColorBinder_UpdateColors_WithAlphaOverride<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });
    }
}
