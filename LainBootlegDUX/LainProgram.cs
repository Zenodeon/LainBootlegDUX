using System;
using LainBootlegDUX.GameContent;
using System.Threading;

namespace LainBootlegDUX
{
    public static class LainProgram
    {
        public static bool inspecting = false;

        [STAThread]
        static void Main()
        {
            DLog.Instantiate();

            using (var scene = new LainInterfaceScene())
                scene.Run();
        }
    }
}
