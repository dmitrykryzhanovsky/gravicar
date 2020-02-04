namespace Gravicar.Communication
{
    public class CommunicationParams
    {
        internal string DroneHostAddress { get; private set; }
        
        internal int SendCommands_CToV_Port { get; private set; }

        internal ECommunicationChannelType SendCommands_CToV_ChannelType { get; private set; }

        public CommunicationParams (string droneHostAddress, 
            int sendCommandsCToDPort, ECommunicationChannelType sendCommandsCToVChannelType)
        {
            DroneHostAddress              = droneHostAddress;
            SendCommands_CToV_Port        = sendCommandsCToDPort;
            SendCommands_CToV_ChannelType = sendCommandsCToVChannelType;
        }
    }
}