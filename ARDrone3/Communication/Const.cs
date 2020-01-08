using System.Runtime.InteropServices;

namespace ARDrone3.Communication
{
    internal static class Const
    {
        internal static readonly int IndexToEncodeFrameTotalSize = Marshal.SizeOf<EFrameDataType> () + Marshal.SizeOf<EFrameTargetBufferId> () + Marshal.SizeOf<byte> ();

        internal static readonly int IndexAfterEncodingFrameHeader = IndexToEncodeFrameTotalSize + Marshal.SizeOf<int> ();
    }
}