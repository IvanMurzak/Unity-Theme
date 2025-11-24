using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests
{
    public partial class TestMultiColorBinder : TestBase
    {
        [UnityTest] public IEnumerator SwitchTheme_Selectable_NoLogs() => TestUtils.RunNoLogs(SwitchTheme_Selectable);
        [UnityTest]
        public IEnumerator SwitchTheme_Selectable() =>
            TestUtils.MultiColorBinder_SwitchTheme<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SwitchTheme_AllStates_Selectable_NoLogs() => TestUtils.RunNoLogs(SwitchTheme_AllStates_Selectable);
        [UnityTest]
        public IEnumerator SwitchTheme_AllStates_Selectable() =>
            TestUtils.MultiColorBinder_SwitchTheme_AllStates<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });

        [UnityTest] public IEnumerator SwitchTheme_WithAlphaOverride_Selectable_NoLogs() => TestUtils.RunNoLogs(SwitchTheme_WithAlphaOverride_Selectable);
        [UnityTest]
        public IEnumerator SwitchTheme_WithAlphaOverride_Selectable() =>
            TestUtils.MultiColorBinder_SwitchTheme_WithAlphaOverride<Selectable, SelectableColorBinder>(target =>
            {
                var cb = target.colors;
                return new[] { cb.normalColor, cb.highlightedColor, cb.pressedColor, cb.selectedColor, cb.disabledColor };
            });
    }
}
