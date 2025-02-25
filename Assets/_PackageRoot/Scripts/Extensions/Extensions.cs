namespace Unity.Theme
{
    public static class Extensions
    {
        public static bool IsNull(this UnityEngine.Object obj) => ReferenceEquals(obj, null) || obj == null;
        public static bool IsNotNull(this UnityEngine.Object obj) => !ReferenceEquals(obj, null) && obj != null;
    }
}