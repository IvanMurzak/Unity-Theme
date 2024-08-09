using System.Text;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class BaseColorBinder : MonoBehaviour
    {
#if UNITY_EDITOR
        protected virtual void OnValidate() => UnityEditor.EditorApplication.delayCall += Validate;
#endif

        protected virtual void Validate()
        {
            if (string.IsNullOrEmpty(data.colorGuid))
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color GUID is <b><color=red>null</color></b> at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
            if (!data.IsConnected)
            {
                if (Theme.Instance?.debugLevel <= DebugLevel.Error)
                    Debug.LogError($"Color with GUID='{data.colorGuid}' not found in database at <b>{GameObjectPath()}</b>", gameObject);
                return;
            }
#if UNITY_EDITOR
            TrySetColor(Theme.Instance.CurrentTheme);
#endif
        }

        // UTILS ---------------------------------------------------------------------------//
        protected string GameObjectPath()                                                   //
        {                                                                                   //
#pragma warning disable CS0168                                                              //
            try { return GameObjectPath(transform).ToString(); }                            //
            catch (MissingReferenceException e) { /* ignore */ }                            //
#pragma warning restore CS0168                                                              //
            return null;                                                                    //
        }                                                                                   //
        protected StringBuilder GameObjectPath(Transform trans, StringBuilder path = null)  //
        {                                                                                   //
            if (path == null)                                                               //
                path = new StringBuilder();                                                 //
                                                                                            //
            if (path.Length == 0)                                                           //
            {                                                                               //
                path.Append(trans.name);                                                    //
            }                                                                               //
            else                                                                            //
            {                                                                               //
                // $"{trans.name}/{path}"                                                   //
                path.Insert(0, "/");                                                        //
                path.Insert(0, trans.name);                                                 //
            }                                                                               //
                                                                                            //
            if (trans.parent == null)                                                       //
            {                                                                               //
                var isPrefab = string.IsNullOrEmpty(trans.gameObject.scene.name);           //
                if (isPrefab)                                                               //
                {                                                                           //
                    path.Insert(0, "<color=cyan>Prefabs</color>/");                         //
                }                                                                           //
                else                                                                        //
                {                                                                           //
                    path.Insert(0, "</color>/");                                            //
                    path.Insert(0, trans.gameObject.scene.name);                            //
                    path.Insert(0, "<color=cyan>");                                         //
                }                                                                           //
                return path;                                                                //
            }                                                                               //
            else                                                                            //
            {                                                                               //
                return GameObjectPath(trans.parent, path);                                  //
            }                                                                               //
        }                                                                                   //
        // =================================================================================//
    }
}