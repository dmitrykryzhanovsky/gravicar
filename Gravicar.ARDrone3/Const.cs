using Gravicar.ARDrone3.Commands;
using Gravicar.ARDrone3.Network;

namespace Gravicar.ARDrone3
{
    internal static class Const
    {
        internal static class Frame
        {
            internal const int HeaderLength = sizeof (EFrameDataType) + sizeof (EFrameTargetBufferId) + sizeof (byte) + sizeof (int);
        }

        internal static class Command
        {
            internal const int HeaderLength = sizeof (ECommandProject) + sizeof (ECommandClass) + sizeof (ECommandId);
        }
    }
}