using System;
using UnityEngine;

namespace _Game.Scripts.Logger
{
    public static class Logger
    {
        private const string LOG_TAG = "[JENGA]";

        public static void Log(object log) => Debug.Log(LOG_TAG + log);
        public static void LogWarning(object log) => Debug.LogWarning(LOG_TAG + log);
        public static void LogError(object log) => Debug.LogError(LOG_TAG + log);
        public static void LogException(Exception exception) => Debug.LogException(exception);
    }
}