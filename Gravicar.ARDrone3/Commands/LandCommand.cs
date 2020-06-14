﻿namespace Gravicar.ARDrone3.Commands
{
    /// <summary>
    /// Команда посадки.
    /// </summary>
    internal class LandCommand : ARDrone3Command
    {
        internal static readonly LandCommand Land = new LandCommand ();

        internal LandCommand () : base (ECommandProject.ARDrone3, ECommandClass.Piloting, ECommandId.Land, Const.Command.ParametersSegmentLength.Land)
        {
        }
    }
}