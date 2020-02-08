namespace Gravicar.ARDrone3.Commands
{
    internal class LandCommand : ARDrone3Command
    {
        internal LandCommand () : base (ECommandProject.ARDrone3, ECommandClass.Piloting, ECommandId.Land)
        {
        }
    }
}