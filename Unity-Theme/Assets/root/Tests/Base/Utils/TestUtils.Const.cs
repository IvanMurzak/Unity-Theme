namespace Unity.Theme.Tests.Base
{
    public static partial class TestUtils
    {
        public static class C_Theme1
        {
            public const string Name = "__Theme1__";

            public static class Color1
            {
                public const string Value = "#FF0000";
                public const string ValueAlternative = "#AA0000";
            }

            public static class Color2
            {
                public const string Value = "#00FF00";
                public const string ValueAlternative = "#00AA00";
            }
        }
        public static class C_Theme2
        {
            public const string Name = "__Theme2__";

            public static class Color1
            {
                public const string Value = "#FFFF00";
                public const string ValueAlternative = "#AAAA00";
            }

            public static class Color2
            {
                public const string Value = "#00FFFF";
                public const string ValueAlternative = "#00AA00";
            }
        }
        public static class C_ThemeNew
        {
            public const string Name = "__ThemeNew__";

            public static class Color1
            {
                public const string Value = "#FFFFAA";
                public const string ValueAlternative = "#AAAA33";
            }

            public static class Color2
            {
                public const string Value = "#33FFFF";
                public const string ValueAlternative = "#33AA33";
            }
        }
        public static class C_Color
        {
            public const string Name_Undefined = "___Undefined___";
            public const string Name_NewColor = "__ColorNew__";
            public const string Name1 = "__Color1__";
            public const string Name2 = "__Color2__";
        }
    }
}