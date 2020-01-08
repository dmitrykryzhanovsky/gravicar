using System.Runtime.InteropServices;

namespace ARDrone3.Commands
{
    internal static class Const
    {
        internal static readonly int IndexToEncodeCommandId = Marshal.SizeOf<ECommandProjectId> () + Marshal.SizeOf<ECommandClassId> ();

        internal static readonly int IndexAfterEncodingCommandHeader = IndexToEncodeCommandId + Marshal.SizeOf<ECommandId> ();
    }
}