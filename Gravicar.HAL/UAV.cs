using System;

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

        private CommunicationController _communicationController;

        private DataThreadController _dataThreadController;

        protected AIVehicle (CommunicationParams communicationParams)
        {
            _sessionStatus = ESessionStatus.Disconnected;

            _communicationController = new CommunicationController (communicationParams);
            _dataThreadController    = new DataThreadController ();

            _dataThreadController.SendCommandToDroneEvent += SendCommandToDrone;
        }

        public bool StartSession ()
        {
            if (_sessionStatus != ESessionStatus.Disconnected) throw new InvalidOperationException ();

            if (_communicationController.StartSession () == true)
            {
                _dataThreadController.StartThread ();

                _sessionStatus = ESessionStatus.Connected;

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
            _dataThreadController.CancelThread ();
            _communicationController.EndSession ();

            _sessionStatus = ESessionStatus.Disconnected;
        }

        public void Dispose ()
        {
            if (_sessionStatus == ESessionStatus.Connected) EndSessionAction ();

            _dataThreadController.Dispose ();
            _communicationController.Dispose ();

            _sessionStatus = ESessionStatus.Disposed;
        }

        protected void PushCommand (DroneCommand command)
        {
            _dataThreadController.PushCommand (command);
        }

        private void SendCommandToDrone (DroneCommand command)
        {
            _communicationController.SendCommandToDrone (command.Encode ());
        }
    }
}