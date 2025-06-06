using System;
using System.Threading;
using UnityEngine;

namespace Unity.Theme.Utils
{
    internal static class Safe
    {
        public static bool Run(Action action, DebugLevel logLevel)
        {
            try
            {
                action?.Invoke();
                return true;
            }
            catch (Exception e)
            {
                if (logLevel.IsActive(DebugLevel.Exception))
                    Debug.LogException(e);

                return false;
            }
        }
        public static bool Run<T>(Action<T> action, T value, DebugLevel logLevel)
        {
            try
            {
                action?.Invoke(value);
                return true;
            }
            catch (Exception e)
            {
                if (logLevel.IsActive(DebugLevel.Exception))
                    Debug.LogException(e);

                return false;
            }
        }
        public static bool Run<T1, T2>(Action<T1, T2> action, T1 value1, T2 value2, DebugLevel logLevel)
        {
            try
            {
                action?.Invoke(value1, value2);
                return true;
            }
            catch (Exception e)
            {
                if (logLevel.IsActive(DebugLevel.Exception))
                    Debug.LogException(e);

                return false;
            }
        }
        public static bool Run(WeakAction action, DebugLevel logLevel)
        {
            try
            {
                action?.Invoke();
                return true;
            }
            catch (Exception e)
            {
                if (logLevel.IsActive(DebugLevel.Exception))
                    Debug.LogException(e);

                return false;
            }
        }
        public static bool Run<T>(WeakAction<T> action, T value, DebugLevel logLevel)
        {
            try
            {
                action?.Invoke(value);
                return true;
            }
            catch (Exception e)
            {
                if (logLevel.IsActive(DebugLevel.Exception))
                    Debug.LogException(e);

                return false;
            }
        }
        public static bool RunCancel(CancellationTokenSource cts, DebugLevel logLevel)
        {
            try
            {
                if (cts == null)
                    return false;

                if (cts.IsCancellationRequested)
                    return false;

                cts.Cancel();
                return true;
            }
            catch (Exception e)
            {
                if (logLevel.IsActive(DebugLevel.Exception))
                    Debug.LogException(e);

                return false;
            }
        }
    }
}