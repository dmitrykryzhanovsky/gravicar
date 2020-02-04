using System.Net.Sockets;

namespace Gravicar.Communication
{
    internal class UdpCommunicationChannel : CommunicationChannel
    {
        private UdpClient _connection;

        internal UdpCommunicationChannel ()
        {
            _connection = new UdpClient ();
        }

        internal override bool OpenConnection (string hostAddress, int port)
        {
            try
            {
                _connection.Connect (hostAddress, port);

                return true;
            }

            catch
            {
                return false;
            }
        }

        internal override void CloseConnection ()
        {
            _connection.Close ();
        }

        public override void Dispose ()
        {
            _connection.Dispose ();
        }

        internal override void SendBytes (byte [] array, int length)
        {
            _connection.Send (array, length);
        }
    }
}