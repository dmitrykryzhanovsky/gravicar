using System;

namespace Gravicar.ARDrone3.Network
{
    internal static class FrameRoutines
    {
        internal static byte [] EncodeFrameHeader (EFrameDataType dataType, EFrameTargetBufferId targetBufferId, int frameTotalSize)
        {
            byte [] frameHeader = new byte [Const.Frame.HeaderLength];

            frameHeader [0] = (byte)dataType;
            frameHeader [1] = (byte)targetBufferId;

            BitConverter.GetBytes (frameTotalSize).CopyTo (frameHeader, 3);

            return frameHeader;
        }
    }
}