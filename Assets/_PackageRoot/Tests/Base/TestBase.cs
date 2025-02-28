using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.Theme.Tests.Base
{
    public partial class TestBase
    {
        DebugLevel startDebugLevel;
        public virtual IEnumerator SetUp()
        {
            //yield return TestUtils.ClearEverything("<b>Test Start </b>");
            startDebugLevel = Theme.Instance.debugLevel;
            Theme.Instance.debugLevel = DebugLevel.Trace;
            TestUtils.BuildTestTheme();
            yield return null;
        }
        public virtual IEnumerator TearDown()
        {
            TestUtils.DeleteTestTheme();
            // yield return TestUtils.ClearEverything("<b>Test End </b>");
            Theme.Instance.debugLevel = startDebugLevel;
            yield return null;
        }
    }
}