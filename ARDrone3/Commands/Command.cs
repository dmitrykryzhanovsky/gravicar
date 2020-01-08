using System;

using ARDrone3.Communication;

namespace ARDrone3.Commands
{
    public abstract class Command : INetworkData
    {
        public abstract EFrameDataType DataType { get; }

        public abstract EFrameTargetBufferId TargetBufferId { get; }

        public virtual int FrameTotalSize
        {
            get
            {
                return Communication.Const.IndexAfterEncodingFrameHeader + Commands.Const.IndexAfterEncodingCommandHeader;
            }
        }

        public abstract ECommandProjectId ProjectId { get; }

        public abstract ECommandClassId ClassId { get; }

        public abstract ECommandId CommandId { get; }

        protected Command ()
        {
        }

        public virtual void EncodeTo (out byte [] array, byte sequenceNumber)
        {
            EncodeFrameAndCommandHeaderTo (out array, sequenceNumber);
        }

        private int EncodeFrameAndCommandHeaderTo (out byte [] array, byte sequenceNumber)
        {
            int index = FrameRoutines.EncodeFrameHeaderTo (out array, DataType, TargetBufferId, sequenceNumber, FrameTotalSize);

            array [index]     = (byte)ProjectId;
            array [index + 1] = (byte)ClassId;

            BitConverter.GetBytes ((ushort)CommandId).CopyTo (array, Const.IndexToEncodeCommandId);

            return index + Const.IndexAfterEncodingCommandHeader;
        }
    }
}