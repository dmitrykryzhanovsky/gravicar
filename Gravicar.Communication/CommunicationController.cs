using System;

using Wormhole.Network;

namespace Gravicar.Communication
{
    public class CommunicationController : IDisposable
    {
        private CommunicationParams _communicationParams;
                
        private UConnection _sendCommandToDroneConnection;

        public CommunicationController (CommunicationParams communicationParams)
        {
            _communicationParams = communicationParams;
            
            _sendCommandToDroneConnection = UConnection.CreateConnection (_communicationParams.SendCommands_CToV_ConnectionType);
        }

        public bool StartSession ()
        {
            return _sendCommandToDroneConnection.OpenConnection (_communicationParams.DroneHostAddress, _communicationParams.SendCommands_CToV_Port);
        }

        public void EndSession ()
        {
            _sendCommandToDroneConnection.CloseConnection ();
        }

        public void Dispose ()
        {
            _sendCommandToDroneConnection.Dispose ();
        }

        public void SendCommandToDrone (Command command)
        {
            _sendCommandToDroneConnection.SendBytes (command.Encode ());
        }
    }
}