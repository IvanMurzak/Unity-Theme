using System.Text;
using UnityEngine;

namespace Unity.Theme
{
    public static class Extensions
    {
        internal static bool IsNull(this UnityEngine.Object obj) => ReferenceEquals(obj, null) || obj == null;
        internal static bool IsNotNull(this UnityEngine.Object obj) => !ReferenceEquals(obj, null) && obj != null;

        public static string GameObjectPath(this Component component)
        {
            if (component.IsNull())
                return null;

#pragma warning disable CS0168
            try { return GameObjectPath(component.transform).ToString(); }
            catch (MissingReferenceException e) { /* ignore */ }
#pragma warning restore CS0168
            return null;
        }
        static StringBuilder GameObjectPath(Transform trans, StringBuilder path = null)
        {
            if (trans.IsNull())
                return null;

            if (path == null)
                path = new StringBuilder();

            if (path.Length == 0)
            {
                path.Append(trans.name);
            }
            else
            {
                path.Insert(0, "/");
                path.Insert(0, trans.name);
            }

            if (trans.parent.IsNull())
            {
                var isPrefab = string.IsNullOrEmpty(trans.gameObject.scene.name);
                if (isPrefab)
                {
                    path.Insert(0, "<color=cyan>Prefabs</color>/");
                }
                else
                {
                    path.Insert(0, "</color>/");
                    path.Insert(0, trans.gameObject.scene.name);
                    path.Insert(0, "<color=cyan>");
                }
                return path;
            }
            else
            {
                return GameObjectPath(trans.parent, path);
            }
        }
    }
}