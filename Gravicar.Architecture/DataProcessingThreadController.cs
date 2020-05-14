using System;

namespace Gravicar.Architecture
{
    public class DataProcessingThreadController : IDisposable
    {
        private CommandThread _sendCommandToDroneThread;

        public event Action<ICommand> SendCommandToDroneEvent;

        public DataProcessingThreadController ()
        {
            _sendCommandToDroneThread = new CommandThread ();

            _sendCommandToDroneThread.SendCommandToDroneEvent += SendCommandToDrone;
        }

        public void StartProcessingThreads ()
        {
            _sendCommandToDroneThread.Start ();
        }

        public void CancelProcessingThreads ()
        {
            _sendCommandToDroneThread.Cancel ();
        }

        public void Dispose ()
        {
            _sendCommandToDroneThread.Dispose ();
        }

        public void PushCommand (ICommand command)
        {
            _sendCommandToDroneThread.PushCommand (command);
        }

        private void SendCommandToDrone (ICommand command)
        {
            SendCommandToDroneEvent (command);
        }
    }
}