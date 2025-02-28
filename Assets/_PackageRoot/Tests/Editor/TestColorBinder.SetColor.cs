using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;
using System.Collections;

namespace Unity.Theme.Tests.Editor
{
    public partial class TestColorBinder : TestBase
    {
        [UnityTest] public IEnumerator ColorBinder_SetColor_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_Image);
        [UnityTest] public IEnumerator ColorBinder_SetColor_Image() => TestUtils.ColorBinder_SetColor_Image<Image, ImageColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SetColor_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_SetColor_TextMeshProUGUI() => TestUtils.ColorBinder_SetColor_Image<TextMeshProUGUI, TextMeshProColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SetColor_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_SetColor_SpriteRenderer() => TestUtils.ColorBinder_SetColor_Image<SpriteRenderer, SpriteRendererColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SetColor_Shadow_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_Shadow);
        [UnityTest] public IEnumerator ColorBinder_SetColor_Shadow() => TestUtils.ColorBinder_SetColor_Image<Shadow, ShadowColorBinder>(target => target.effectColor);

        [UnityTest] public IEnumerator ColorBinder_SetColor_Outline_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SetColor_Outline);
        [UnityTest] public IEnumerator ColorBinder_SetColor_Outline() => TestUtils.ColorBinder_SetColor_Image<Outline, OutlineColorBinder>(target => target.effectColor);
    }
}