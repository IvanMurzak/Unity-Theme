using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;

namespace Unity.Theme.Tests.Editor
{
    public partial class TestColorBinder : TestBase
    {
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_Image);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Image() => TestUtils.ColorBinder_SwitchColor<Image, ImageColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_TextMeshProUGUI() => TestUtils.ColorBinder_SwitchColor<TextMeshProUGUI, TextMeshProColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_SpriteRenderer() => TestUtils.ColorBinder_SwitchColor<SpriteRenderer, SpriteRendererColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Shadow_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_Shadow);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Shadow() => TestUtils.ColorBinder_SwitchColor<Shadow, ShadowColorBinder>(target => target.effectColor);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Outline_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_Outline);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Outline() => TestUtils.ColorBinder_SwitchColor<Outline, OutlineColorBinder>(target => target.effectColor);
    }
}