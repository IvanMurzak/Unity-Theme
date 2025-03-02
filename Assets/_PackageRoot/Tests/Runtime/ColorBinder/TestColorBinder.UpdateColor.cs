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
        [UnityTest] public IEnumerator UpdateColor_Image_NoLogs() => TestUtils.RunNoLogs(UpdateColor_Image);
        [UnityTest] public IEnumerator UpdateColor_Image() => TestUtils.ColorBinder_UpdateColor<Image, ImageColorBinder>(target => target.color);

        [UnityTest] public IEnumerator UpdateColor_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(UpdateColor_TextMeshProUGUI);
        [UnityTest] public IEnumerator UpdateColor_TextMeshProUGUI() => TestUtils.ColorBinder_UpdateColor<TextMeshProUGUI, TextMeshProColorBinder>(target => target.color);

        [UnityTest] public IEnumerator UpdateColor_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(UpdateColor_SpriteRenderer);
        [UnityTest] public IEnumerator UpdateColor_SpriteRenderer() => TestUtils.ColorBinder_UpdateColor<SpriteRenderer, SpriteRendererColorBinder>(target => target.color);

        [UnityTest] public IEnumerator UpdateColor_Shadow_NoLogs() => TestUtils.RunNoLogs(UpdateColor_Shadow);
        [UnityTest] public IEnumerator UpdateColor_Shadow() => TestUtils.ColorBinder_UpdateColor<Shadow, ShadowColorBinder>(target => target.effectColor);

        [UnityTest] public IEnumerator UpdateColor_Outline_NoLogs() => TestUtils.RunNoLogs(UpdateColor_Outline);
        [UnityTest] public IEnumerator UpdateColor_Outline() => TestUtils.ColorBinder_UpdateColor<Outline, OutlineColorBinder>(target => target.effectColor);
    }
}