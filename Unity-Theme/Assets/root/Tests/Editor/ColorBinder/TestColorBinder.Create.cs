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
        [UnityTest] public IEnumerator Create_Image_NoLogs() => TestUtils.RunNoLogs(Create_Image);
        [UnityTest]
        public IEnumerator Create_Image()
        {
            TestUtils.CreateGenericColorBinder<Image, ImageColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator Create_TextMeshProUGUI_NoLogs() => TestUtils.RunNoLogs(Create_TextMeshProUGUI);
        [UnityTest]
        public IEnumerator Create_TextMeshProUGUI()
        {
            TestUtils.CreateGenericColorBinder<TextMeshProUGUI, TextMeshProColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator Create_SpriteRenderer_NoLogs() => TestUtils.RunNoLogs(Create_SpriteRenderer);
        [UnityTest]
        public IEnumerator Create_SpriteRenderer()
        {
            TestUtils.CreateGenericColorBinder<SpriteRenderer, SpriteRendererColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator Create_Shadow_NoLogs() => TestUtils.RunNoLogs(Create_Shadow);
        [UnityTest]
        public IEnumerator Create_Shadow()
        {
            TestUtils.CreateGenericColorBinder<Shadow, ShadowColorBinder>(out var target);
            yield return null;
        }

        [UnityTest] public IEnumerator Create_Outline_NoLogs() => TestUtils.RunNoLogs(Create_Outline);
        [UnityTest]
        public IEnumerator Create_Outline()
        {
            TestUtils.CreateGenericColorBinder<Outline, OutlineColorBinder>(out var target);
            yield return null;
        }
    }
}