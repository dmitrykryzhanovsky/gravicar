using Gravicar.ARDrone3.Commands;
using Gravicar.ARDrone3.Communication;

namespace Gravicar.ARDrone3
{
    internal static class Const
    {
        internal static class Communication
        {
            private const int FrameHeaderBeginningSize = sizeof (EFrameDataType) + sizeof (EFrameTargetBufferId) + sizeof (byte);

            internal const int IndexToEncodeFrameTotalSize = FrameHeaderBeginningSize;

            internal const int FrameHeaderSize = FrameHeaderBeginningSize + sizeof (int);

            internal const int IndexAfterEncodingFrameHeader = FrameHeaderSize;

            internal const int AcknowledgmentFrameTotalSize = FrameHeaderSize + sizeof (byte);
        }

        internal static class Commands
        {
            private const int CommandHeaderBeginningSize = sizeof (ECommandProjectId) + sizeof (ECommandClassId);

            internal const int IndexToEncodeCommandId = Communication.IndexAfterEncodingFrameHeader + CommandHeaderBeginningSize;

            private const int CommandHeaderSize = CommandHeaderBeginningSize + sizeof (ECommandId);

            internal const int CommandNoParametersSize = Communication.FrameHeaderSize + CommandHeaderSize;

            internal const int IndexToEncodeCommandParameters = Communication.IndexAfterEncodingFrameHeader + CommandHeaderSize;
        }
    }
}