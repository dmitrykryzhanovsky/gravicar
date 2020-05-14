using Gravicar.ARDrone3.Commands;

namespace Gravicar.ARDrone3
{
    public abstract class ARDrone3UAV : UAV
    {
        protected ARDrone3UAV () : base (null)
        {
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
            PushCommand (TakeOffCommand.TakeOff);
        }

        public void Land ()
        {
            PushCommand (LandCommand.Land);
        }
    }
}