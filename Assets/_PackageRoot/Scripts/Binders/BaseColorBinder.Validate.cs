using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class BaseColorBinder : MonoBehaviour
    {
        protected virtual void OnValidate()
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
        protected string GameObjectPath() => GameObjectPath(transform);                     //
        protected static string GameObjectPath(Transform trans, string path = "")           //
        {                                                                                   //
            if (string.IsNullOrEmpty(path))                                                 //
                path = trans.name;                                                          //
            else                                                                            //
                path = $"{trans.name}/{path}";                                              //
                                                                                            //
            if (trans.parent == null)                                                       //
            {                                                                               //
                var isPrefab = string.IsNullOrEmpty(trans.gameObject.scene.name);           //
                if (isPrefab)                                                               //
                    path = $"<color=cyan>Prefabs</color>/{path}";                           //
                else                                                                        //
                    path = $"<color=cyan>{trans.gameObject.scene.name}</color>/{path}";     //
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