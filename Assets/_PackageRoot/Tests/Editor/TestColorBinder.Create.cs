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
        [UnityTest] public IEnumerator ColorBinder_Create_Image_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_Image);
        [UnityTest] public IEnumerator ColorBinder_Create_Image()
        {
            TestUtils.CreateGenericColorBinder<Image, ImageColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_Create_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_TextMeshProUGUI);
        [UnityTest] public IEnumerator ColorBinder_Create_TextMeshProUGUI()
        {
            TestUtils.CreateGenericColorBinder<TextMeshProUGUI, TextMeshProColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_Create_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_SpriteRenderer);
        [UnityTest] public IEnumerator ColorBinder_Create_SpriteRenderer()
        {
            TestUtils.CreateGenericColorBinder<SpriteRenderer, SpriteRendererColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_Create_Shadow_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_Shadow);
        [UnityTest] public IEnumerator ColorBinder_Create_Shadow()
        {
            TestUtils.CreateGenericColorBinder<Shadow, ShadowColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator ColorBinder_Create_Outline_NoLogs() => TestUtils.RunNoLogs(ColorBinder_Create_Outline);
        [UnityTest] public IEnumerator ColorBinder_Create_Outline()
        {
            TestUtils.CreateGenericColorBinder<Outline, OutlineColorBinder>(out var target);
            yield return null;
        }
    }
}