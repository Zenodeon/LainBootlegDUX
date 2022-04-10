using System;
using Lain_Bootleg_DUX.GameContent;
using System.Threading;

namespace Lain_Bootleg_DUX
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
