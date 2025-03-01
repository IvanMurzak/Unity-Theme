using Unity.Theme.Tests.Base;
using UnityEngine.UI;
using Unity.Theme.Binders;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;

namespace Unity.Theme.Tests.Runtime
{
    public partial class TestColorBinder : TestBase
    {
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_Image);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Image() => TestUtils.ColorBinder_SwitchTheme<Image, ImageColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_TextMeshProUGUI() => TestUtils.ColorBinder_SwitchTheme<TextMeshProUGUI, TextMeshProColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_SpriteRenderer() => TestUtils.ColorBinder_SwitchTheme<SpriteRenderer, SpriteRendererColorBinder>(target => target.color);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Shadow_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_Shadow);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Shadow() => TestUtils.ColorBinder_SwitchTheme<Shadow, ShadowColorBinder>(target => target.effectColor);

        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Outline_NoLogs() => TestUtils.RunNoLogs(ColorBinder_SwitchTheme_Outline);
        [UnityTest] public IEnumerator ColorBinder_SwitchTheme_Outline() => TestUtils.ColorBinder_SwitchTheme<Outline, OutlineColorBinder>(target => target.effectColor);
    }
}