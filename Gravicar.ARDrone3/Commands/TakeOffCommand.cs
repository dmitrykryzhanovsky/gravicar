﻿namespace Gravicar.ARDrone3.Commands
{
    internal class TakeOffCommand : ARDrone3Command
    {
        internal static readonly TakeOffCommand TakeOff = new TakeOffCommand ();

        internal TakeOffCommand () : base (ECommandProject.ARDrone3, ECommandClass.Piloting, ECommandId.TakeOff, Const.Command.ParametersSegmentLength.TakeOff)
        {
        }
    }
}