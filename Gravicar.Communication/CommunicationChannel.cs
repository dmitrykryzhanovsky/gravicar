using System;

namespace Gravicar.Communication
{
    internal abstract class CommunicationChannel : IDisposable
    {
        internal static CommunicationChannel CreateChannel (ECommunicationChannelType communicationChannelType)
        {
            if (communicationChannelType == ECommunicationChannelType.Udp) return new UdpCommunicationChannel ();
            else return new TcpCommunicationChannel ();
        }

        internal abstract bool OpenConnection (string hostAddress, int port);

        internal abstract void CloseConnection ();

        public abstract void Dispose ();

        internal abstract void SendBytes (byte [] array, int length);
    }
}