using System;

using Gravicar.Architecture;
using Gravicar.Communication;

namespace Gravicar
{
    public abstract class UAV : IDisposable
    {
        private enum ESessionStatus : int
        {
            Disconnected,

            Connected,

            Disposed
        }

        private ESessionStatus _sessionStatus;

        private DataProcessingThreadController _dataProcessingThreadController;
        private CommunicationController        _communicationController;

        protected UAV (CommunicationParams communicationParams)
        {
            _sessionStatus = ESessionStatus.Disconnected;

            _dataProcessingThreadController = new DataProcessingThreadController ();
            _communicationController        = new CommunicationController (communicationParams);

            _dataProcessingThreadController.SendCommandToDroneEvent += SendCommandToDrone;
        }

        public bool StartSession ()
        {
            if (_sessionStatus != ESessionStatus.Disconnected) throw new InvalidOperationException ();

            if (_communicationController.StartSession () == true)
            {
                _sessionStatus = ESessionStatus.Connected;

                _dataProcessingThreadController.StartProcessingThreads ();                

                return true;
            }

            return false;
        }

        public void EndSession ()
        {
            if (_sessionStatus != ESessionStatus.Connected) throw new InvalidOperationException ();

            EndSessionAction ();
        }

        private void EndSessionAction ()
        {
            _dataProcessingThreadController.CancelProcessingThreads ();
            _communicationController.EndSession ();

            _sessionStatus = ESessionStatus.Disconnected;
        }

        public void Dispose ()
        {
            if (_sessionStatus == ESessionStatus.Connected) EndSessionAction ();

            _dataProcessingThreadController.Dispose ();
            _communicationController.Dispose ();

            _sessionStatus = ESessionStatus.Disposed;
        }

        protected void PushCommand (Command command)
        {
            _dataProcessingThreadController.PushCommand (command);
        }

        private void SendCommandToDrone (Command command)
        {
            _communicationController.SendCommandToDrone (command);
        }
    }
}