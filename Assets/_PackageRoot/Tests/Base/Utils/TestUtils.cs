using System.Collections;
using System;
using System.Collections.Generic;

namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        public const string Theme1Name = "Theme1";
        public const string Theme2Name = "Theme2";
        public const string Color1Name = "Color1";
        public const string Color2Name = "Color2";
        public const string Color1Theme1Value = "#FF0000";
        public const string Color2Theme1Value = "#00FF00";
        public const string Color1Theme2Value = "#FFFF00";
        public const string Color2Theme2Value = "#00FFFF";

        public static Theme BuildTestTheme()
        {
            var theme = new Theme();

            theme.AddTheme(Theme1Name, setCurrent: true);
            theme.AddColor(Color1Name, Color1Theme1Value);
            theme.AddColor(Color2Name, Color2Theme1Value);

            theme.AddTheme(Theme2Name, setCurrent: true);
            theme.AddColor(Color1Name, Color1Theme2Value);
            theme.AddColor(Color2Name, Color2Theme2Value);

            return theme;
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