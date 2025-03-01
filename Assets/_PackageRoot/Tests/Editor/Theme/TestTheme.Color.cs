using System.Collections;
using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.Theme.Tests
{
    public partial class TestTheme : TestBase
    {
        [UnityTest] public IEnumerator SetColor_NoLogs() => TestUtils.RunNoLogs(SetColor);
        [UnityTest] public IEnumerator SetColor()
        {
            LogAssert.Expect(LogType.Error, $"[Theme] SetColor error. Color with name '{TestUtils.C_Color.Name_Undefined}' not found");
            Theme.Instance.SetColor(TestUtils.C_Color.Name_Undefined, Color.red);
            yield return null;
        }
    }
}