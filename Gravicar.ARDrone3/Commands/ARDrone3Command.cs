using System;

using Gravicar.ARDrone3.Network;

namespace Gravicar.ARDrone3.Commands
{
    internal abstract class ARDrone3Command : Command
    {
        private byte [] _frameHeader;
        private byte [] _commandHeader;

        private int FrameTotalSize
        {
            get
            {
                return Const.Frame.HeaderLength + Const.Command.HeaderLength + CommandParametersSize;
            }
        }

        protected virtual int CommandParametersSize
        {
            get
            {
                return 0;
            }
        }

        protected ARDrone3Command (ECommandProject commandProject, ECommandClass commandClass, ECommandId commandId) : base ()
        {
            _frameHeader   = FrameRoutines.EncodeFrameHeader (EFrameDataType.Common, EFrameTargetBufferId.CToV_Common, FrameTotalSize);
            _commandHeader = EncodeCommandHeader (commandProject, commandClass, commandId);
        }

        public override byte [] Encode ()
        {
            byte [] encoded = new byte [FrameTotalSize];

            _frameHeader.CopyTo (encoded, 0);
            _commandHeader.CopyTo (encoded, Const.Frame.HeaderLength);

            EncodeCommandParametersTo (encoded, Const.Frame.HeaderLength + Const.Command.HeaderLength);

            return encoded;
        }

        private byte [] EncodeCommandHeader (ECommandProject commandProject, ECommandClass commandClass, ECommandId commandId)
        {
            byte [] commandHeader = new byte [Const.Command.HeaderLength];

            commandHeader [0] = (byte)commandProject;
            commandHeader [1] = (byte)commandClass;

            BitConverter.GetBytes ((ushort)commandId).CopyTo (commandHeader, 2);

            return _commandHeader;
        }

        protected virtual void EncodeCommandParametersTo (byte [] encoded, int index)
        {
        }
    }
}