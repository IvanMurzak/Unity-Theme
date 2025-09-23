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
        const float alpha = 0.2f;

        [UnityTest] public IEnumerator SetColor_OverrideAlpha_Image_NoLogs() => TestUtils.RunNoLogs(SetColor_OverrideAlpha_Image);
        [UnityTest] public IEnumerator SetColor_OverrideAlpha_Image() => TestUtils.ColorBinder_SetColor_OverrideAlpha<Image, ImageColorBinder>(target => target.color);

        [UnityTest] public IEnumerator SetColor_OverrideAlpha_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(SetColor_OverrideAlpha_TextMeshProUGUI);
        [UnityTest] public IEnumerator SetColor_OverrideAlpha_TextMeshProUGUI() => TestUtils.ColorBinder_SetColor_OverrideAlpha<TextMeshProUGUI, TextMeshProColorBinder>(target => target.color);

        [UnityTest] public IEnumerator SetColor_OverrideAlpha_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(SetColor_OverrideAlpha_SpriteRenderer);
        [UnityTest] public IEnumerator SetColor_OverrideAlpha_SpriteRenderer() => TestUtils.ColorBinder_SetColor_OverrideAlpha<SpriteRenderer, SpriteRendererColorBinder>(target => target.color);

        [UnityTest] public IEnumerator SetColor_OverrideAlpha_Shadow_NoLogs() => TestUtils.RunNoLogs(SetColor_OverrideAlpha_Shadow);
        [UnityTest] public IEnumerator SetColor_OverrideAlpha_Shadow() => TestUtils.ColorBinder_SetColor_OverrideAlpha<Shadow, ShadowColorBinder>(target => target.effectColor);

        [UnityTest] public IEnumerator SetColor_OverrideAlpha_Outline_NoLogs() => TestUtils.RunNoLogs(SetColor_OverrideAlpha_Outline);
        [UnityTest] public IEnumerator SetColor_OverrideAlpha_Outline() => TestUtils.ColorBinder_SetColor_OverrideAlpha<Outline, OutlineColorBinder>(target => target.effectColor);
    }
}