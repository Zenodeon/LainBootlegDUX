using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace LainBootlegDUX
{
    public static class DLog
    {
        public static void Log(string message) => DebugLogger.Wpf.DLog.Log(message);
        public static void Warn(string message) => DebugLogger.Wpf.DLog.Warn(message);
        public static void Alert(string message) => DebugLogger.Wpf.DLog.Alert(message);

        public static void Instantiate()
        {
            DebugLogger.Wpf.DLog.Instantiate();

            FNALoggerEXT.LogInfo = (msg) => Log($"FNA : {msg}");
            FNALoggerEXT.LogWarn = (msg) => Warn($"FNA : {msg}");
            FNALoggerEXT.LogError = (msg) => Alert($"FNA : {msg}");
        }
    }
}
