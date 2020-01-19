using System.Runtime.InteropServices;

using Gravicar.ARDrone3.Commands;
using Gravicar.ARDrone3.Communication;

namespace Gravicar.ARDrone3
{
    internal static class Const
    {
        internal static class Communication
        {
            internal static readonly int IndexToEncodeFrameTotalSize = Marshal.SizeOf<EFrameDataType> () + Marshal.SizeOf<EFrameTargetBufferId> () + 
                Marshal.SizeOf<byte> ();

            internal static readonly int FrameHeaderSize = IndexToEncodeFrameTotalSize + Marshal.SizeOf<int> ();

            internal static readonly int AcknowledgmentFrameTotalSize = FrameHeaderSize + Marshal.SizeOf<byte> ();            
        }

        internal static class Commands
        {
            internal static readonly int IndexToEncodeCommandId = Marshal.SizeOf<ECommandProjectId> () + Marshal.SizeOf<ECommandClassId> ();

            internal static readonly int CommandHeaderSize = IndexToEncodeCommandId + Marshal.SizeOf<ECommandId> ();

            internal static readonly int CommandNoParametersSize = Communication.FrameHeaderSize + CommandHeaderSize;
        }
    }
}