using System;

namespace Gravicar.Communication
{
    public class CommunicationController : IDisposable
    {
        private CommunicationParams _communicationParams;

        private CommunicationChannel _sendCommandsToDroneChannel;        

        public CommunicationController (CommunicationParams communicationParams)
        {
            _communicationParams = communicationParams;

            _sendCommandsToDroneChannel = CommunicationChannel.CreateChannel (_communicationParams.SendCommands_CToV_ChannelType);
        }

        public bool StartSession ()
        {
            return _sendCommandsToDroneChannel.OpenConnection (_communicationParams.DroneHostAddress, _communicationParams.SendCommands_CToV_Port);
        }

        public void EndSession ()
        {
            _sendCommandsToDroneChannel.CloseConnection ();
        }

        public void Dispose ()
        {
            _sendCommandsToDroneChannel.Dispose ();
        }

        public void SendCommandToDrone (byte [] array)
        {
            _sendCommandsToDroneChannel.SendBytes (array, array.Length);
        }
    }
}