using System;

using Gravicar.ARDrone3.Communication;

namespace Gravicar.ARDrone3.Commands
{
    public abstract class Command : INetworkFrame
    {
        public abstract EFrameDataType DataType { get; }

        public abstract EFrameTargetBufferId TargetBufferId { get; }

        public byte SequenceNumber { get; set; }

        public int TotalSize
        {
            get
            {
                return Const.Commands.CommandNoParametersSize + CommandParametersSize;
            }
        }

        protected virtual int CommandParametersSize
        {
            get
            {
                return 0;
            }
        }

        public abstract ECommandProjectId ProjectId { get; }

        public abstract ECommandClassId ClassId { get; }

        public abstract ECommandId CommandId { get; }

        public void EncodeTo (byte [] array, byte sequenceNumber)
        {
            int index = FrameRoutines.EncodeFrameHeaderTo (this, array, sequenceNumber);

            array [index]     = (byte)ProjectId;
            array [index + 1] = (byte)ClassId;

            BitConverter.GetBytes ((ushort)CommandId).CopyTo (array, Const.Commands.IndexToEncodeCommandId);

            EncodeCommandParametersTo (array, Const.Commands.CommandNoParametersSize);
        }

        protected virtual void EncodeCommandParametersTo (byte [] array, int index)
        {
        }
    }
}