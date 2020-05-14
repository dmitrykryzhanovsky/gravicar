using System;

using Gravicar.ARDrone3.Commands;

namespace Gravicar.ARDrone3.Network
{
    internal static class FrameKit
    {
        internal static void EncodeFrameHeaderTo (byte [] array, 
            EFrameDataType dataType, EFrameTargetBufferId targetBufferId, byte sequenceNumber, int frameTotalSize)
        {
            array [0] = (byte)dataType;
            array [1] = (byte)targetBufferId;
            array [2] = sequenceNumber;

            BitConverter.GetBytes (frameTotalSize).CopyTo (array, 3);
        }

        internal static void EncodeCommandHeaderTo (byte [] array, int index, 
            ECommandProject commandProject, ECommandClass commandClass, ECommandId commandId)
        {
            array [index]     = (byte)commandProject;
            array [index + 1] = (byte)commandClass;

            BitConverter.GetBytes ((ushort)commandId).CopyTo (array, index + 2);
        }
    }
}