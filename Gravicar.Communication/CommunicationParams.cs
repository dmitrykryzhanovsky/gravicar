using Wormhole.Network;

namespace Gravicar.Communication
{
    public class CommunicationParams
    {
        internal string DroneHostAddress { get; private set; }
        
        internal int SendCommands_CToV_Port { get; private set; }

        internal EConnectionType SendCommands_CToV_ConnectionType { get; private set; }

        internal CommunicationParams (string droneHostAddress, 
            int sendCommandsCToVPort, EConnectionType sendCommandsCToVConnectionType)
        {
            DroneHostAddress                 = droneHostAddress;
            SendCommands_CToV_Port           = sendCommandsCToVPort;
            SendCommands_CToV_ConnectionType = sendCommandsCToVConnectionType;
        }
    }
}