using System;

namespace Gravicar.ARDrone3.Communication
{
    internal static class FrameRoutines
    {
        internal static int EncodeFrameHeaderTo (INetworkFrame frameToEncode, byte [] array, byte sequenceNumber)
        {
            frameToEncode.SequenceNumber = sequenceNumber;

            array [0] = (byte)frameToEncode.DataType;
            array [1] = (byte)frameToEncode.TargetBufferId;
            array [2] = sequenceNumber;

            BitConverter.GetBytes (frameToEncode.TotalSize).CopyTo (array, Const.Communication.IndexToEncodeFrameTotalSize);

            return Const.Communication.FrameHeaderSize;
        }
    }
}