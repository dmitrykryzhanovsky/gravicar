﻿using Gravicar.ARDrone3.Communication;

namespace Gravicar.ARDrone3.Commands
{
    public class TakeOffCommand : Command
    {
        public override EFrameDataType DataType
        {
            get
            {
                return EFrameDataType.Common;
            }
        }

        public override EFrameTargetBufferId TargetBufferId
        {
            get
            {
                return EFrameTargetBufferId.CToV_Common;
            }
        }

        public override ECommandProjectId ProjectId
        {
            get
            {
                return ECommandProjectId.ARDrone3;
            }
        }

        public override ECommandClassId ClassId
        {
            get
            {
                return ECommandClassId.Piloting;
            }
        }

        public override ECommandId CommandId
        {
            get
            {
                return ECommandId.TakeOff;
            }
        }

        public TakeOffCommand ()
        {
        }
    }
}