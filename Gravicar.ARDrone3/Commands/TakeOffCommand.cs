namespace Gravicar.ARDrone3.Commands
{
    internal class TakeOffCommand : ARDrone3Command
    {
        internal TakeOffCommand () : base (ECommandProject.ARDrone3, ECommandClass.Piloting, ECommandId.TakeOff)
        {
        }
    }
}