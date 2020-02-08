using Gravicar.ARDrone3.Commands;
using Gravicar.HAL;
using Gravicar.Infrastructure;

namespace Gravicar.ARDrone3
{
    public abstract class ARDrone3Vehicle : Vehicle
    {
        private CommandChannel _sendCommandToDroneChannel;

        protected ARDrone3Vehicle () : base ()
        {
            _sendCommandToDroneChannel = new CommandChannel ();
        }

        /// <summary>
        /// Выполняет первоначальный поиск дрона при установке сетевого соединения.
        /// </summary>
        /// <returns><list type="bullet">
        /// <item>TRUE – операция прошла успешно, соединение установлено корректно.</item>
        /// <item>FALSE – что-то пошло не так.</item>
        /// </list></returns>
        public bool Discover ()
        {
            throw new System.NotImplementedException ();
        }

        public void TakeOff ()
        {
            _sendCommandToDroneChannel.PushCommand (new TakeOffCommand ());
        }

        public void Land ()
        {
            _sendCommandToDroneChannel.PushCommand (new LandCommand ());
        }
    }
}