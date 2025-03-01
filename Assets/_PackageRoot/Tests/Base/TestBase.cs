using System.Collections;
using UnityEngine;

namespace Unity.Theme.Tests.Base
{
    public partial class TestBase
    {
        DebugLevel startDebugLevel;
        string startThemeName;
        public virtual IEnumerator SetUp()
        {
            Debug.Log($"<b>Test Start </b> ------------------------------------------");
            startDebugLevel = Theme.Instance.debugLevel;
            startThemeName = Theme.Instance.CurrentThemeName;
            Theme.Instance.debugLevel = DebugLevel.Trace;
            TestUtils.BuildTestTheme();
            Theme.Instance.CurrentThemeName = TestUtils.Theme1Name;
            yield return null;
        }
        public virtual IEnumerator TearDown()
        {
            yield return null;
            TestUtils.DeleteTestTheme();
            yield return null;
            Theme.Instance.debugLevel = startDebugLevel;
            Theme.Instance.CurrentThemeName = startThemeName;
            Debug.Log($"<b>Test End </b> --------------------------------------------");
        }
    }
}