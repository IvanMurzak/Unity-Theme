using System;
using System.Collections.Generic;

namespace Unity.Theme.Tests.Base
{
    public partial class TestBase
    {
        public virtual void SetUp()
        {
            //yield return TestUtils.ClearEverything("<b>Test Start </b>");
            Theme.Instance.debugLevel = DebugLevel.Trace;
        }
        public virtual void TearDown()
        {
            // yield return TestUtils.ClearEverything("<b>Test End </b>");
        }
    }
}