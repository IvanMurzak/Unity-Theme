using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator SetColors_OverrideAlpha_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_OverrideAlpha_Selectable);
        [UnityTest]
        public IEnumerator SetColors_OverrideAlpha_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_OverrideAlpha<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SetColors_OverrideAlpha_ByLabel_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_OverrideAlpha_ByLabel_Selectable);
        [UnityTest]
        public IEnumerator SetColors_OverrideAlpha_ByLabel_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_OverrideAlpha_ByLabel<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            }, "Normal");

        [UnityTest] public IEnumerator SetColors_MultipleAlphaOverrides_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_MultipleAlphaOverrides_Selectable);
        [UnityTest]
        public IEnumerator SetColors_MultipleAlphaOverrides_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_MultipleAlphaOverrides<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SetColors_AlphaOverride_InvalidIndex_Selectable_NoLogs() => TestUtils.RunNoLogs(SetColors_AlphaOverride_InvalidIndex_Selectable);
        [UnityTest]
        public IEnumerator SetColors_AlphaOverride_InvalidIndex_Selectable() =>
            TestUtils.MultiColorBinder_SetColors_AlphaOverride_InvalidIndex<Selectable, SelectableColorBinder>();
    }
}
