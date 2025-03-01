using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests
{
    public partial class TestColorBinder : TestBase
    {
        [UnityTest] public IEnumerator SwitchColor_Image_NoLogs() => TestUtils.RunNoLogs(SwitchColor_Image);
        [UnityTest] public IEnumerator SwitchColor_Image() => TestUtils.ColorBinder_SwitchColor<Image, ImageColorBinder>(target => target.color);

        [UnityTest] public IEnumerator SwitchColor_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(SwitchColor_TextMeshProUGUI);
        [UnityTest] public IEnumerator SwitchColor_TextMeshProUGUI() => TestUtils.ColorBinder_SwitchColor<TextMeshProUGUI, TextMeshProColorBinder>(target => target.color);

        [UnityTest] public IEnumerator SwitchColor_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(SwitchColor_SpriteRenderer);
        [UnityTest] public IEnumerator SwitchColor_SpriteRenderer() => TestUtils.ColorBinder_SwitchColor<SpriteRenderer, SpriteRendererColorBinder>(target => target.color);

        [UnityTest] public IEnumerator SwitchColor_Shadow_NoLogs() => TestUtils.RunNoLogs(SwitchColor_Shadow);
        [UnityTest] public IEnumerator SwitchColor_Shadow() => TestUtils.ColorBinder_SwitchColor<Shadow, ShadowColorBinder>(target => target.effectColor);

        [UnityTest] public IEnumerator SwitchColor_Outline_NoLogs() => TestUtils.RunNoLogs(SwitchColor_Outline);
        [UnityTest] public IEnumerator SwitchColor_Outline() => TestUtils.ColorBinder_SwitchColor<Outline, OutlineColorBinder>(target => target.effectColor);
    }
}