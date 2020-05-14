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

            internal const int FrameAndCommandHeaderLength = Frame.HeaderLength + Command.HeaderLength;

            internal static class ParametersSegmentLength
            {
                internal const int TakeOff = 0;

                internal const int Land = 0;
            }
        }
    }
}