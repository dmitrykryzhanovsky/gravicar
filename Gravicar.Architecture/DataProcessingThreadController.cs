using System;

namespace Gravicar.Architecture
{
    public class DataProcessingThreadController : IDisposable
    {
        private CommandThread _sendCommandToDroneThread;

        public DataProcessingThreadController ()
        {
            _sendCommandToDroneThread = new CommandThread ();
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
    }
}