using System;
using UnityEngine;

namespace Unity.Theme.Binders
{
    public abstract partial class LogableMonoBehaviour : MonoBehaviour
    {
        protected string GameObjectPath() => Extensions.GameObjectPath(this);

        protected void LogTrace(string message, params object[] args)
        {
            if (!Theme.IsLogActive(DebugLevel.Trace))
                return;

            if (this.IsNotNull())
                Debug.Log($"[Theme] {string.Format(message, args)} at <b>{GameObjectPath()}</b>", gameObject);
            else
                Debug.Log($"[Theme] {string.Format(message, args)} at <b>[Destroyed Object]</b>");
        }

        protected void Log(string message, params object[] args)
        {
            if (!Theme.IsLogActive(DebugLevel.Log))
                return;

            if (this.IsNotNull())
                Debug.Log($"[Theme] {string.Format(message, args)} at <b>{GameObjectPath()}</b>", gameObject);
            else
                Debug.Log($"[Theme] {string.Format(message, args)} at <b>[Destroyed Object]</b>");
        }

        protected void LogWarning(string message, params object[] args)
        {
            if (!Theme.IsLogActive(DebugLevel.Warning))
                return;

            if (this.IsNotNull())
                Debug.LogWarning($"[Theme] {string.Format(message, args)} at <b>{GameObjectPath()}</b>", gameObject);
            else
                Debug.LogWarning($"[Theme] {string.Format(message, args)} at <b>[Destroyed Object]</b>");
        }

        protected void LogError(string message, params object[] args)
        {
            if (!Theme.IsLogActive(DebugLevel.Error))
                return;

            if (this.IsNotNull())
                Debug.LogError($"[Theme] {string.Format(message, args)} at <b>{GameObjectPath()}</b>", gameObject);
            else
                Debug.LogError($"[Theme] {string.Format(message, args)} at <b>[Destroyed Object]</b>");
        }

        protected void LogException(Exception exception)
        {
            if (!Theme.IsLogActive(DebugLevel.Exception))
                return;

            if (this.IsNotNull())
                Debug.LogException(exception, gameObject);
            else
                Debug.LogException(exception);
        }
    }
}