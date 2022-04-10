using System;
using System.Collections.Generic;
using System.Text;

namespace Lain_Bootleg_DUX
{
    public static class DLog
    {
        public static void Instantiate() => DebugLogger.Wpf.DLog.Instantiate();
        public static void Log(string message) => DebugLogger.Wpf.DLog.Log(message);
        public static void Warn(string message) => DebugLogger.Wpf.DLog.Warn(message);
        public static void Alert(string message) => DebugLogger.Wpf.DLog.Alert(message);
    }
}
