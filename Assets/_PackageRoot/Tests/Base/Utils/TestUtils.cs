using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        public const string Theme1Name = "__Theme1";
        public const string Theme2Name = "__Theme2";
        public const string Color1Name = "__Color1";
        public const string Color2Name = "__Color2";
        public const string Color1Theme1Value = "#FF0000";
        public const string Color2Theme1Value = "#00FF00";
        public const string Color1Theme2Value = "#FFFF00";
        public const string Color2Theme2Value = "#00FFFF";

        public static void BuildTestTheme()
        {
            Theme.Instance.SetOrAddTheme(Theme1Name, setCurrent: true);
            Theme.Instance.SetOrAddColor(Color1Name, Color1Theme1Value);
            Theme.Instance.SetOrAddColor(Color2Name, Color2Theme1Value);

            Theme.Instance.SetOrAddTheme(Theme2Name, setCurrent: true);
            Theme.Instance.SetOrAddColor(Color1Name, Color1Theme2Value);
            Theme.Instance.SetOrAddColor(Color2Name, Color2Theme2Value);
        }
        public static void DeleteTestTheme()
        {
            Theme.Instance.RemoveColorByName(Color1Name);
            Theme.Instance.RemoveColorByName(Color2Name);
            Theme.Instance.RemoveTheme(Theme1Name);
            Theme.Instance.RemoveTheme(Theme2Name);
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