using System.Collections;
using System;

namespace Unity.Theme.Tests.Editor
{
    public static partial class TestUtils
    {
        public static IEnumerator RunNoLogs(Func<IEnumerator> test)
        {
            Theme.Instance.debugLevel = DebugLevel.Error;
            yield return test();
        }
        public static void RunNoLogs(Action test)
        {
            Theme.Instance.debugLevel = DebugLevel.Error;
            test();
        }
    }
}