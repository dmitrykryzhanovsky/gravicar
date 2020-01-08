using System;

namespace ARDrone3.Communication
{
    internal static class FrameRoutines
    {
        internal static int EncodeFrameHeaderTo (out byte [] array, EFrameDataType dataType, EFrameTargetBufferId targetBufferId, byte sequenceNumber, 
            int frameTotalSize)
        {
            array = new byte [frameTotalSize];

            array [0] = (byte)dataType;
            array [1] = (byte)targetBufferId;
            array [2] = sequenceNumber;

            BitConverter.GetBytes (frameTotalSize).CopyTo (array, Const.IndexToEncodeFrameTotalSize);

            return Const.IndexAfterEncodingFrameHeader;
        }
    }
}