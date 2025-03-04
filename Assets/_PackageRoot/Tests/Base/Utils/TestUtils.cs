using System.Collections;
using System;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        public static void BuildTestTheme()
        {
            Theme.Instance.SetOrAddTheme(C_Theme1.Name, setCurrent: true);
            Theme.Instance.SetOrAddColor(C_Color.Name1, C_Theme1.Color1.Value);
            Theme.Instance.SetOrAddColor(C_Color.Name2, C_Theme1.Color2.Value);

            Theme.Instance.SetOrAddTheme(C_Theme2.Name, setCurrent: true);
            Theme.Instance.SetOrAddColor(C_Color.Name1, C_Theme2.Color1.Value);
            Theme.Instance.SetOrAddColor(C_Color.Name2, C_Theme2.Color2.Value);
        }
        public static void DeleteTestTheme()
        {
            Theme.Instance.RemoveTheme(C_Theme1.Name);
            Theme.Instance.RemoveTheme(C_Theme2.Name);
            Theme.Instance.RemoveColorByName(C_Color.Name1);
            Theme.Instance.RemoveColorByName(C_Color.Name2);
        }

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