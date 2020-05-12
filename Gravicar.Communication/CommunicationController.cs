using System;

using Wormhole.Network;

namespace Gravicar.Communication
{
    public class CommunicationController : IDisposable
    {
        private CommunicationParams _communicationParams;

        private CommandChannel _sendCommandToDroneChannel;
        private UConnection    _sendCommandToDroneConnection;

        public CommunicationController (CommunicationParams communicationParams)
        {
            _communicationParams = communicationParams;

            _sendCommandToDroneChannel    = new CommandChannel ();
            _sendCommandToDroneConnection = UConnection.CreateConnection (_communicationParams.SendCommands_CToV_ConnectionType);
        }

        public bool StartSession ()
        {
            if (_sendCommandToDroneConnection.OpenConnection (_communicationParams.DroneHostAddress, _communicationParams.SendCommands_CToV_Port))
            {
                _sendCommandToDroneChannel.Start ();

                return true;
            }

            else return false;
        }

        public void EndSession ()
        {
            _sendCommandToDroneChannel.Cancel ();

            _sendCommandToDroneConnection.CloseConnection ();            
        }

        public void Dispose ()
        {
            _sendCommandToDroneChannel.Dispose ();

            _sendCommandToDroneConnection.Dispose ();
        }
    }
}