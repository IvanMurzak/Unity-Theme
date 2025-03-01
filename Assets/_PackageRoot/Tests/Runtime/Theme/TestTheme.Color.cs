using System.Collections;
using NUnit.Framework;
using Unity.Theme.Tests.Base;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.Theme.Tests
{
    public partial class TestTheme : TestBase
    {
        const string ColorName_Undefined = "___Undefined___";
        [UnityTest] public IEnumerator SetColor_NoLogs() => TestUtils.RunNoLogs(SetColor);
        [UnityTest] public IEnumerator SetColor()
        {
            LogAssert.Expect(LogType.Error, $"[Theme] SetColor error. Color with name '{ColorName_Undefined}' not found");
            Theme.Instance.SetColor(ColorName_Undefined, Color.red);
            yield return null;
        }
    }
}